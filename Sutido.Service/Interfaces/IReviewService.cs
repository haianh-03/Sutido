// Sutido.Service.Interfaces.IReviewService
using Sutido.Model.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sutido.Service.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(long id); // <-- Sửa từ int
        Task AddAsync(Review entity);
        Task UpdateAsync(Review entity);
        Task DeleteAsync(Review entity); // <-- Sửa từ int id

        // ✅ Thêm mới:
        Task<IEnumerable<Review>> GetReviewsForUserAsync(long userId);
    }
}