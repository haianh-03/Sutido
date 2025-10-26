using Sutido.Model.Entites;
using Sutido.Repository.UnitOfWork;
using Sutido.Service.Interfaces;

namespace Sutido.Service.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Booking>> GetAllAsync() =>
            await _unitOfWork.Bookings.GetAllAsync();

        public async Task<Booking?> GetByIdAsync(long id) =>
            await _unitOfWork.Bookings.GetByIdAsync(id);

        public async Task CreateAsync(Booking booking)
        {
            await _unitOfWork.Bookings.AddAsync(booking);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Booking booking)
        {
            _unitOfWork.Bookings.Update(booking);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await _unitOfWork.Bookings.GetByIdAsync(id);
            if (entity != null)
            {
                _unitOfWork.Bookings.Delete(entity);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
