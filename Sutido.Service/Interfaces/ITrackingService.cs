using Sutido.Model.Entites;

namespace Sutido.Service.Interfaces
{
    public interface ITrackingService
    {
        Task<Tracking?> GetByIdAsync(long id); // <-- Sửa sang long
        Task AddAsync(Tracking entity);
        Task<IEnumerable<Tracking>> GetByBookingIdAsync(long bookingId);

        // ❌ Bỏ UpdateAsync và DeleteAsync
        // Task UpdateAsync(Tracking entity);
        // Task DeleteAsync(long id);
    }
}