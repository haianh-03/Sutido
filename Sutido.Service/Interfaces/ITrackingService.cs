using Sutido.Model;
using Sutido.Model.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sutido.Service.Interfaces
{
    public interface ITrackingService
    {
        Task<IEnumerable<Tracking>> GetAllAsync();
        Task<Tracking> GetByIdAsync(int id);
        Task AddAsync(Tracking entity);
        Task UpdateAsync(Tracking entity);
        Task DeleteAsync(int id);
    }
}
