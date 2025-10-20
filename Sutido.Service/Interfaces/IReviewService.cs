using Sutido.Model;
using Sutido.Model.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sutido.Service.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review> GetByIdAsync(int id);
        Task AddAsync(Review entity);
        Task UpdateAsync(Review entity);
        Task DeleteAsync(int id);
    }
}
