using Microsoft.EntityFrameworkCore;
using Sutido.Model.Data;
using Sutido.Model.Entites;
using Sutido.Model.Enums;
using Sutido.Repository.Generic;
using Sutido.Repository.Interfaces;

namespace Sutido.Repository.Implementations
{
    public class CertificationRepository : GenericRepo<Certification>, ICertificationRepo
    {
        public CertificationRepository(SutidoProjectContext context) : base(context) { }

        public async Task<IEnumerable<Certification>> GetAllByTutorProfileIdAsync(long id, StatusType status)
        {
            try
            {
                return await _context.Certifications
                    .Where(c => c.TutorProfileId == id)
                    .Where(c => c.Status == status)
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
