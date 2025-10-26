using Microsoft.AspNetCore.Http;
using Sutido.Model.Entites;
using Sutido.Model.Enums;
using Sutido.Repository.UnitOfWork;
using Sutido.Service.Interfaces;
using System.Runtime.ConstrainedExecution;

namespace Sutido.Service.Implementations
{
    public class CertificationService : ICertificationService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IStorageService _iStorageService;

        public CertificationService(IUnitOfWork iUnitOfWork, IStorageService iStorageService)
        {
            _iUnitOfWork = iUnitOfWork;
            _iStorageService = iStorageService;
        }

        public async Task<IEnumerable<Certification>?> GetAllAsync(long id)
        {
            var profile = await _iUnitOfWork.TutorProfiles.GetByIdAsync(id);
            if (profile == null) return null;
            var certs = await _iUnitOfWork.Certifications.GetAllByTutorProfileIdAsync(id, profile.Status);

            foreach (var cert in certs)
            {
                if (!string.IsNullOrEmpty(cert.FileUrl))
                {
                    var fileUrl = await _iStorageService.GetFileUrlAsync(cert.FileUrl, isPublic: false);
                    cert.FileUrl = fileUrl;
                }
            }

            return certs;
        }

        public async Task<int> UploadAndAddAsync(long tutorProfileId, string documentType, string? note, IFormFile file)
        {
            var profile = await _iUnitOfWork.TutorProfiles.GetByIdAsync(tutorProfileId);
            if (profile == null) throw new Exception("Tutor profile is not exist.");

            if (profile.Status != StatusType.Approved) throw new Exception("Invalid tutor profile.");

            if (file == null || file.Length == 0)
                throw new Exception("Invalid file.");

            var folder = $"certifications/{profile.UserId}";
            var filePath = await _iStorageService.UploadFileAsync(file, isPublic: false, folder: folder);

            var cert = new Certification
            {
                TutorProfileId = tutorProfileId,
                DocumentType = documentType,
                Note = note,
                FileUrl = filePath
            };

            await _iUnitOfWork.Certifications.AddAsync(cert);
            return await _iUnitOfWork.SaveAsync();
        }


        public async Task<int> DeleteAsync(long id)
        {
            var certification = await _iUnitOfWork.Certifications.GetByIdAsync(id);

            if(certification == null) throw new InvalidOperationException("Default certification not found.");

            if(certification.Status == StatusType.Approved)
            {
                bool deleted = await _iStorageService.DeleteFileAsync(certification.FileUrl, false);
                if (!deleted)
                {
                    Console.WriteLine($"Cannot delete file: {certification.FileUrl}");
                }
            }

            _iUnitOfWork.Certifications.Remove(certification);

            return await _iUnitOfWork.SaveAsync();
        }


        public async Task<int> ReviewCertificationAsync(Certification c)
        {
            var cert = await _iUnitOfWork.Certifications.GetByIdAsync(c.CertificationId);
            if (cert == null) throw new InvalidOperationException("Default certification not found.");

            cert.ReviewedBy = c.ReviewedBy;
            cert.ReviewedAt = DateTimeOffset.UtcNow;
            cert.Status = c.Status;

            if(cert.Status == StatusType.Rejected)
            {
                bool deleted = await _iStorageService.DeleteFileAsync(cert.FileUrl, false);
                if (!deleted)
                {
                    Console.WriteLine($"Cannot delete file: {cert.FileUrl}");
                }
            }
            _iUnitOfWork.Certifications.Update(cert);
            return await _iUnitOfWork.SaveAsync();
        }
    }
}
