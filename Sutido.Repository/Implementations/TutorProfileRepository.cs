using Microsoft.EntityFrameworkCore;
using PRN232.Lab2.CoffeeStore.Repositories.Extensions;
using Sutido.Model.Data;
using Sutido.Model.Entites;
using Sutido.Model.Enums;
using Sutido.Repository.Generic;
using Sutido.Repository.Interfaces;

namespace Sutido.Repository.Implementations
{
    public class TutorProfileRepository : GenericRepo<TutorProfile>, ITutorProfileRepo
    {
        public TutorProfileRepository(SutidoProjectContext context) : base(context) { }

        public async Task<TutorProfile?> GetByUserIdAsync(long id)
        {
            try
            {
                var profile = await _context.TutorProfiles
                    .Include(t => t.Certifications)
                    .SingleOrDefaultAsync(t => t.UserId == id);

                if (profile != null)
                {
                    profile.Certifications = profile.Certifications
                        .Where(c => c.Status == profile.Status)
                        .ToList();
                }

                return profile;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TutorProfile?> GetByTutorIdAsync(long id)
        {
            try
            {
                return await _context.TutorProfiles
                    .Include(t => t.Certifications)
                    .SingleOrDefaultAsync(t => t.TutorProfileId == id);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TutorProfile?> GetByTutorIdAsync(long id, StatusType status)
        {
            try
            {
                return await _context.TutorProfiles
                    .Include(t => t.Certifications.Where(c => c.Status == status))
                    .Where(t => t.Status == status)
                    .SingleOrDefaultAsync(t => t.TutorProfileId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<TutorProfile>> GetAllListAsync(string? sortBy, string? sortOrder, int page, int pageSize, Dictionary<string, string>? filters)
        {
            try
            {

                var query = _context.TutorProfiles.AsQueryable();

                    query = query
                    .Include(t => t.Certifications)
                    .Sort(sortBy, sortOrder)
                    .Paging(page, pageSize)
                    .Filter(filters);

                var result = await query.ToListAsync();
                foreach (var t in result)
                {
                    t.Certifications = t.Certifications
                        .Where(c => c.Status == t.Status)
                        .ToList();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
