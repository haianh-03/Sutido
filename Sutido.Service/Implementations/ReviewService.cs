// Sutido.Service.Implementations.ReviewService
using Sutido.Model.Entites;
using Sutido.Repository.Interfaces;
using Sutido.Repository.UnitOfWork;
using Sutido.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sutido.Service.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IReviewRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Review>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<Review?> GetByIdAsync(long id) => // <-- Sửa từ int
            await _repository.GetByIdAsync(id);

        public async Task<IEnumerable<Review>> GetReviewsForUserAsync(long userId) =>
            await _repository.GetReviewsForUserAsync(userId);

        public async Task AddAsync(Review entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(Review entity)
        {
            _repository.Update(entity);
            await _unitOfWork.SaveAsync();
        }

        // ✅ Sửa: Hiệu quả hơn, tránh 2 lần fetch
        public async Task DeleteAsync(Review entity)
        {
            _repository.Delete(entity);
            await _unitOfWork.SaveAsync();
        }
    }
}