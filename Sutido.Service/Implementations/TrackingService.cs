using Sutido.Model.Entites;
using Sutido.Repository.Interfaces;
using Sutido.Repository.UnitOfWork;
using Sutido.Service.Interfaces;

namespace Sutido.Service.Implementations
{
    public class TrackingService : ITrackingService
    {
        private readonly ITrackingRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TrackingService(ITrackingRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Tracking?> GetByIdAsync(long id) => // <-- Sửa sang long
            await _repository.GetByIdAsync(id);

        public async Task<IEnumerable<Tracking>> GetByBookingIdAsync(long bookingId) =>
            await _repository.GetByBookingIdAsync(bookingId);

        public async Task AddAsync(Tracking entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        // ❌ Xóa UpdateAsync và DeleteAsync
    }
}