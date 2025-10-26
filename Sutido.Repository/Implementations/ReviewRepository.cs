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

        public async Task<IEnumerable<Review>> GetAllAsync() =>
            await _context.Reviews.ToListAsync();

        public async Task<Review> GetByIdAsync(int id) =>
            await _context.Reviews.FindAsync(id);

        public async Task AddAsync(Review entity) =>
            await _context.Reviews.AddAsync(entity);

        public void Update(Review entity) =>
            _context.Reviews.Update(entity);

        public void Delete(Review entity) =>
            _context.Reviews.Remove(entity);
    }
}
