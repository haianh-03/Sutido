using Microsoft.EntityFrameworkCore;
using Sutido.Model.Data;
using Sutido.Model.Entites;
using Sutido.Repository.Interfaces;

namespace Sutido.Repository.Implementations
{
    public class TrackingRepository : ITrackingRepository
    {
        private readonly SutidoProjectContext _context;

        public TrackingRepository(SutidoProjectContext context)
        {
            _context = context;
        }

        // ✅ Sửa: GetById (long) và .Include()
        public async Task<Tracking?> GetByIdAsync(long id) => // <-- Sửa sang long
            await _context.Trackings
                .Include(t => t.TutorUser) // Tải kèm user
                .FirstOrDefaultAsync(t => t.TrackingId == id);

        // ✅ Thêm mới: GetByBookingId
        public async Task<IEnumerable<Tracking>> GetByBookingIdAsync(long bookingId) =>
            await _context.Trackings
                .Where(t => t.BookingId == bookingId)
                .Include(t => t.TutorUser) // Tải kèm user
                .OrderBy(t => t.ActionAt) // Sắp xếp theo thời gian
                .ToListAsync();

        public async Task AddAsync(Tracking entity)
        {
            await _context.Trackings.AddAsync(entity);
        }

        // ❌ Xóa GetAll, Update, Delete
    }
}