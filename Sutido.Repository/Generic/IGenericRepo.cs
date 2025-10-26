namespace Sutido.Repository.Generic
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(string? keyword, string? sortBy, string? sortOrder, int page, int pageSize, Dictionary<string, string>? filters);
        Task<T?> GetByIdAsync(long id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
