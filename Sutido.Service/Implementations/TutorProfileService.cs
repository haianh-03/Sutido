using Azure.Core;
using Microsoft.AspNetCore.Http;
using Sutido.Model.Entites;
using Sutido.Model.Enums;
using Sutido.Repository.UnitOfWork;
using Sutido.Service.Interfaces;

namespace Sutido.Service.Implementations
{
    public class TutorProfileService : ITutorProfileService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IStorageService _iStorageService;

        public TutorProfileService(IUnitOfWork iUnitOfWork, IStorageService iStorageService)
        {
            _iUnitOfWork = iUnitOfWork;
            _iStorageService = iStorageService;
        }

        public async Task<int> AddAsync(TutorProfile profile)
        {

            await _iUnitOfWork.TutorProfiles.AddAsync(profile);
            return await _iUnitOfWork.SaveAsync();
        }

        public async Task<int> UploadAndAddAsync(TutorProfile profile, List<string> docs, List<string> notes, List<IFormFile> files)
        {
            // check user
            var user = await _iUnitOfWork.Users.GetByIdAsync(profile.UserId);
            if (user == null) throw new Exception("user is not exist.");

            // check existing profile
            var existingProfile = await _iUnitOfWork.TutorProfiles.GetByUserIdAsync(profile.UserId);

            // check certification
            if (files == null || files.Count == 0) throw new Exception("No files provided.");
            if (docs.Count != files.Count || notes.Count != files.Count)
                throw new Exception("Certifications data mismatch.");

            var certifications = new List<Certification>();

            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                var docType = docs[i];
                var note = notes[i];

                if (file == null || file.Length == 0)
                    throw new Exception($"File #{i} is invalid.");

                // upload file lên storage
                var folder = $"certifications/{profile.UserId}";
                var filePath = await _iStorageService.UploadFileAsync(file, isPublic: false, folder: folder);

                // tạo Certification object
                var certification = new Certification
                {
                    DocumentType = docType,
                    Note = note,
                    FileUrl = filePath
                };

                certifications.Add(certification);
            }

            if(existingProfile == null)
            {
                profile.Certifications = certifications;
                await _iUnitOfWork.TutorProfiles.AddAsync(profile);
            }
            else
            {
                if (existingProfile.Status == StatusType.Suspended) throw new Exception("tutor profile has been banned.");

                existingProfile.Description = profile.Description;
                existingProfile.Education = profile.Education;
                existingProfile.ExperienceYears = profile.ExperienceYears;
                existingProfile.CreatedAt = DateTimeOffset.UtcNow;
                existingProfile.Status = StatusType.Pending;
                existingProfile.Reason = null;
                existingProfile.Certifications = certifications;
                existingProfile.ReviewedAt = null;
                existingProfile.ReviewerBy = null;

                _iUnitOfWork.TutorProfiles.Update(existingProfile);
            }

            return await _iUnitOfWork.SaveAsync();
        }

        public async Task<TutorProfile?> GetProfileByIdAsync(long id, bool isReview)
        {
            TutorProfile? profile;
            if (isReview)
            {
                profile = await _iUnitOfWork.TutorProfiles.GetByTutorIdAsync(id, StatusType.Pending);
            }
            else
            {
                profile = await _iUnitOfWork.TutorProfiles.GetByTutorIdAsync(id, StatusType.Approved);
            }
            if (profile == null)
                return null;

            foreach (var cert in profile.Certifications)
            {
                if (!string.IsNullOrEmpty(cert.FileUrl))
                {
                    var fileUrl = await _iStorageService.GetFileUrlAsync(cert.FileUrl, isPublic: false);
                    cert.FileUrl = fileUrl;
                }
            }
            return profile;
        }

        public async Task<TutorProfile?> GetProfileByUserIdAsync(long id)
        {
            var profile =  await _iUnitOfWork.TutorProfiles.GetByUserIdAsync(id);

            if (profile == null) return null;

            if(profile.Status != StatusType.Approved) return null;

            foreach (var cert in profile.Certifications)
            {
                if (!string.IsNullOrEmpty(cert.FileUrl))
                {
                    var fileUrl = await _iStorageService.GetFileUrlAsync(cert.FileUrl, isPublic: false);
                    cert.FileUrl = fileUrl;
                }
            }
            return profile;
        }

        public async Task<int> UpdateAsync(long id, TutorProfile t)
        {
            var profile = await _iUnitOfWork.TutorProfiles.GetByIdAsync(id);

            if (profile == null) throw new InvalidOperationException("Default tutor profile not found.");

            profile.Description = t.Description;
            profile.Education = t.Education;
            profile.ExperienceYears = t.ExperienceYears;

            _iUnitOfWork.TutorProfiles.Update(profile);
            return await _iUnitOfWork.SaveAsync();
        }

        public async Task<int> ReviewTutorProfileAsync(TutorProfile t)
        {
            var profile = await _iUnitOfWork.TutorProfiles.GetByTutorIdAsync(t.TutorProfileId);
            if (profile == null) throw new InvalidOperationException("Default tutor profile not found.");

            profile.ReviewerBy = t.ReviewerBy;
            profile.ReviewedAt = DateTimeOffset.UtcNow;
            profile.Status = t.Status;

            switch (t.Status)
            {
                case StatusType.Approved:
                    {
                        foreach (var cert in profile.Certifications)
                        {
                            cert.Status = StatusType.Approved;
                            cert.ReviewedBy = t.ReviewerBy;
                        }
                        break;
                    }
                case StatusType.Rejected:
                    {
                        foreach(var cert in profile.Certifications)
                        {
                            cert.Status = StatusType.Rejected;
                            cert.ReviewedBy = t.ReviewerBy;
                            bool deleted = await _iStorageService.DeleteFileAsync(cert.FileUrl, false);
                            if (!deleted)
                            {
                                Console.WriteLine($"Cannot delete file: {cert.FileUrl}");
                            }
                        }
                        profile.Reason = t.Reason;
                        break;
                    }
                case StatusType.Suspended:
                    {
                        foreach (var cert in profile.Certifications)
                        {
                            cert.Status = StatusType.Approved;
                            cert.ReviewedBy = t.ReviewerBy;
                        }
                        break;
                    }
                default:
                    throw new InvalidOperationException("Invalid status");
            }

            _iUnitOfWork.TutorProfiles.Update(profile);
            return await _iUnitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<TutorProfile>> GetAllAsync(string? sortBy, string? sortOrder, int page, int pageSize, Dictionary<string, string>? filters)
        {
            var profiles = await _iUnitOfWork.TutorProfiles.GetAllListAsync(sortBy, sortOrder, page, pageSize, filters);
            if (profiles == null || !profiles.Any())
                return Enumerable.Empty<TutorProfile>();

            foreach (var profile in profiles)
            {
                foreach (var cert in profile.Certifications)
                {
                    if (!string.IsNullOrEmpty(cert.FileUrl))
                    {
                        var fileUrl = await _iStorageService.GetFileUrlAsync(cert.FileUrl, isPublic: false);
                        cert.FileUrl = fileUrl;
                    }
                }
            }

            return profiles;
        }
    }
}
