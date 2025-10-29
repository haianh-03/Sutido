// Sutido.Repository.Interfaces.IReviewRepository
using Sutido.Model.Entites;

namespace Sutido.Repository.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(long id); // <-- Sửa từ int
        Task AddAsync(Review entity);
        void Update(Review entity);
        void Delete(Review entity);

        // ✅ Thêm mới: Lấy review cho một user
        Task<IEnumerable<Review>> GetReviewsForUserAsync(long userId);
    }
}