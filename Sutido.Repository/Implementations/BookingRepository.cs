using Microsoft.EntityFrameworkCore;
using Sutido.Model;
using Sutido.Model.Entites;
using Sutido.Repository.Interfaces;

namespace Sutido.Repository.Implementations
{
    public class BookingRepository : IBookingRepository
    {
        private readonly SutidoProjectContext _context;

        public BookingRepository(SutidoProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync() =>
            await _context.Bookings.ToListAsync();

        public async Task<Booking?> GetByIdAsync(long id) =>
            await _context.Bookings.FindAsync(id);

        public async Task AddAsync(Booking booking) =>
            await _context.Bookings.AddAsync(booking);

        public void Update(Booking booking) =>
            _context.Bookings.Update(booking);

        public void Delete(Booking booking) =>
            _context.Bookings.Remove(booking);
    }
}
