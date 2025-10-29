using Sutido.Model.Entites;

namespace Sutido.Repository.Interfaces
{
    public interface ITrackingRepository
    {
        // Task<IEnumerable<Tracking>> GetAllAsync(); // ⚠️ Chúng ta sẽ bỏ hàm này
        Task<Tracking?> GetByIdAsync(long id); // <-- Sửa sang long
        Task AddAsync(Tracking entity);

        // ✅ Thêm hàm mới
        Task<IEnumerable<Tracking>> GetByBookingIdAsync(long bookingId);

        // ❌ Bỏ Update và Delete
        // void Update(Tracking entity);
        // void Delete(Tracking entity);
    }
}