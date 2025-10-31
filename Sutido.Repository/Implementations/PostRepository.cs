using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.Repositories.Extensions;
using Sutido.Model.Data;
using Sutido.Model.Entites;
using Sutido.Model.Enums;
using Sutido.Repository.Generic;
using Sutido.Repository.Interfaces;

namespace Sutido.Repository.Implementations
{
    public class PostRepository : GenericRepo<Post>, IPostRepository
    {
        public PostRepository(SutidoProjectContext context) : base(context) { }

        public async Task<IEnumerable<Post>> GetAllAsync(string? keyword, string? sortBy, string? sortOrder, int page, int pageSize, Dictionary<string, string>? filters, PostType type)
        {
            try
            {
                var query = _dbSet.AsQueryable();
                query = query
                    .Where(q => q.PostType == type)
                    .Search(keyword, "Title, Description")
                    .Sort(sortBy, sortOrder)
                    .Paging(page, pageSize)
                    .Filter(filters);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
