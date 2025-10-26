using Sutido.Model.Entites;

namespace Sutido.Service.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync(string? keyword, string? sortBy, string? sortOrder, int page, int pageSize, Dictionary<string, string>? filters);
        Task<User?> GetByIdAsync(long id);
        Task<User?> GetUserByIdAsync(long id);
        Task<int> AddAsync(User u);
        Task<int> UpdateAsync(long id, User u);
        Task<int> SoftDeleteUserAsync(long id);
    }
}
