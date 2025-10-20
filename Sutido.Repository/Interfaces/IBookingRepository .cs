using Sutido.Model.Entites;

namespace Sutido.Repository.Interfaces
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(long id);
        Task AddAsync(Booking booking);
        void Update(Booking booking);
        void Delete(Booking booking);
    }
}
