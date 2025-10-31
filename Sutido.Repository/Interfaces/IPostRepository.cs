using Sutido.Model.Entites;
using Sutido.Model.Enums;
using Sutido.Repository.Generic;

namespace Sutido.Repository.Interfaces
{
    public interface IPostRepository : IGenericRepo<Post>
    {
        Task<IEnumerable<Post>> GetAllAsync(string? keyword, string? sortBy, string? sortOrder, int page, int pageSize, Dictionary<string, string>? filters, PostType type);
    }
}
