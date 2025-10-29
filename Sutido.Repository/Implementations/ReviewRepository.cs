// Sutido.Repository.Implementations.ReviewRepository
using Microsoft.EntityFrameworkCore;
using Sutido.Model.Data;
using Sutido.Model.Entites;
using Sutido.Repository.Interfaces;

namespace Sutido.Repository.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly SutidoProjectContext _context;

        public ReviewRepository(SutidoProjectContext context)
        {
            _context = context;
        }

        // ✅ Sửa: Tải kèm user
        public async Task<IEnumerable<Review>> GetAllAsync() =>
            await _context.Reviews
                .Include(r => r.FromUser)
                .Include(r => r.ToUser)
                .ToListAsync();

        // ✅ Sửa: T_ải kèm user và đổi sang long
        public async Task<Review?> GetByIdAsync(long id) => // <-- Sửa từ int
            await _context.Reviews
                .Include(r => r.FromUser)
                .Include(r => r.ToUser)
                .FirstOrDefaultAsync(r => r.ReviewId == id);

        // ✅ Thêm mới:
        public async Task<IEnumerable<Review>> GetReviewsForUserAsync(long userId) =>
            await _context.Reviews
                .Where(r => r.ToUserId == userId) // Lấy các review *về* user này
                .Include(r => r.FromUser)
                .Include(r => r.ToUser)
                .ToListAsync();

        public async Task AddAsync(Review entity)
        {
            await _context.Reviews.AddAsync(entity);
        }

        public void Update(Review entity)
        {
            _context.Reviews.Update(entity);
        }

        public void Delete(Review entity)
        {
            _context.Reviews.Remove(entity);
        }
    }
}