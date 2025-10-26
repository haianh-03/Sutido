using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.Repositories.Extensions;
using Sutido.Model.Data;

namespace Sutido.Repository.Generic
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly SutidoProjectContext _context;
        protected DbSet<T> _dbSet;

        public GenericRepo(SutidoProjectContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(string? keyword, string? sortBy, string? sortOrder, int page, int pageSize, Dictionary<string, string>? filters)
        {
            try
            {
                var query = _dbSet.AsQueryable();
                query = query
                    .Search(keyword, "Name")
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

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<T?> GetByIdAsync(long id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
