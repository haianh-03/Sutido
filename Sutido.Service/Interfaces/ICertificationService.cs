using Microsoft.AspNetCore.Http;
using Sutido.Model.Entites;

namespace Sutido.Service.Interfaces
{
    public interface ICertificationService
    {
        Task<IEnumerable<Certification>?> GetAllAsync(long id);
        Task<int> UploadAndAddAsync(long tutorProfileId, string documentType, string? note, IFormFile file);
        Task<int> DeleteAsync(long id);
        Task<int> ReviewCertificationAsync(Certification c);
    }
}
