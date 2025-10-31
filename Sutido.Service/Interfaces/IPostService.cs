using Sutido.Model.Entites;
using Sutido.Model.Enums;

namespace Sutido.Service.Interfaces
{
    public interface IPostService
    {
        //Task<IEnumerable<Post>> GetAllAsync(
        //    string? keyword,
        //    string? sortBy,
        //    string sortOrder,
        //    int page,
        //    int pageSize, 
        //    Dictionary<string, string>? filters);
        Task<IEnumerable<Post>> GetAllAsync(
            string? keyword,
            string? sortBy,
            string sortOrder,
            int page,
            int pageSize,
            Dictionary<string, string>? filters,
            PostType type);
        Task<Post?> GetDetailsByIdAsync(int id);
        Task<Post?> AddAsync(Post p);
        Task<int> UpdateAsync(int id, Post p);
        Task<int> DeleteAsync(int id);
    }
}
