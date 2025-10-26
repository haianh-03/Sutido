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

        public async Task<IEnumerable<Tracking>> GetAllAsync() =>
            await _context.Trackings.ToListAsync();

        public async Task<Tracking> GetByIdAsync(int id) =>
            await _context.Trackings.FindAsync(id);

        public async Task AddAsync(Tracking entity) =>
            await _context.Trackings.AddAsync(entity);

        public void Update(Tracking entity) =>
            _context.Trackings.Update(entity);

        public void Delete(Tracking entity) =>
            _context.Trackings.Remove(entity);
    }
}
