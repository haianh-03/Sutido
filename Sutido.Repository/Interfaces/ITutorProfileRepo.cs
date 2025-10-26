using Sutido.Model.Entites;
using Sutido.Model.Enums;
using Sutido.Repository.Generic;

namespace Sutido.Repository.Interfaces
{
    public interface ITutorProfileRepo : IGenericRepo<TutorProfile>
    {
        Task<TutorProfile?> GetByUserIdAsync(long id);
        Task<TutorProfile?> GetByTutorIdAsync(long id);
        Task<TutorProfile?> GetByTutorIdAsync(long id, StatusType status);
        Task<IEnumerable<TutorProfile>> GetAllListAsync(string? sortBy, string? sortOrder, int page, int pageSize, Dictionary<string, string>? filters);
    }
}
