using Sutido.Model;
using Sutido.Model.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sutido.Repository.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int id);
        Task AddAsync(Review entity);
        void Update(Review entity);
        void Delete(Review entity);
    }
}

