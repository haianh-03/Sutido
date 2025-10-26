using Microsoft.AspNetCore.Http;
using Sutido.Model.Entites;

namespace Sutido.Service.Interfaces
{
    public interface ITutorProfileService
    {
        Task<int> AddAsync(TutorProfile profile);
        Task<int> UploadAndAddAsync(TutorProfile profile, List<string> docs, List<string> notes, List<IFormFile> files);
        Task<TutorProfile?> GetProfileByIdAsync(long id, bool i);
        Task<TutorProfile?> GetProfileByUserIdAsync(long id);
        Task<int> UpdateAsync(long id, TutorProfile t);
        Task<int> ReviewTutorProfileAsync(TutorProfile t);
        Task<IEnumerable<TutorProfile>> GetAllAsync(string? sortBy, string? sortOrder, int page, int pageSize, Dictionary<string, string>? filters);
    }
}
