using Sutido.Model;
using Sutido.Model.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sutido.Repository.Interfaces
{
    public interface ITrackingRepository
    {
        Task<IEnumerable<Tracking>> GetAllAsync();
        Task<Tracking> GetByIdAsync(int id);
        Task AddAsync(Tracking entity);
        void Update(Tracking entity);
        void Delete(Tracking entity);
    }
}
