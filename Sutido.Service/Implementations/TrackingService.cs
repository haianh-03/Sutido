using Sutido.Model;
using Sutido.Model.Entites;
using Sutido.Repository.Interfaces;
using Sutido.Repository.UnitOfWork;
using Sutido.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Tracking>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task<Tracking> GetByIdAsync(int id) => await _repository.GetByIdAsync(id);

        public async Task AddAsync(Tracking entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Tracking entity)
        {
            _repository.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity != null)
            {
                _repository.Delete(entity);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
