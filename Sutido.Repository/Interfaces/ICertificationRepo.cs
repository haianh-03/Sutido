using Sutido.Model.Entites;
using Sutido.Model.Enums;
using Sutido.Repository.Generic;

namespace Sutido.Repository.Interfaces
{
    public interface ICertificationRepo : IGenericRepo<Certification>
    {
        Task<IEnumerable<Certification>> GetAllByTutorProfileIdAsync(long id, StatusType status);
    }
}
