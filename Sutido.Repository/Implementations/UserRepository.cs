using Microsoft.EntityFrameworkCore;
using Sutido.Model.Data;
using Sutido.Model.Entites;
using Sutido.Repository.Generic;
using Sutido.Repository.Interfaces;

namespace Sutido.Repository.Implementations
{
    public class UserRepository : GenericRepo<User>, IUserRepo
    {

        public UserRepository(SutidoProjectContext context) : base(context) { }

        public async Task<User?> GetProfileAsync(long id)
        {
            try
            {
                return await _context.Users
                .Include(u => u.Point)
                .Where(u => u.IsActive == true)
                .FirstOrDefaultAsync(u => u.UserId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User?> FindUserByEmailAsync(string email, bool isActive)
        {
            try
            {
                if (isActive)
                {
                    return await _context.Users
                        .Where(u => u.IsActive == isActive)
                        .FirstOrDefaultAsync(u => u.Email == email);
                }
                return await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == email);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<User?> FindUserByPhoneAsync(string phone, bool isActive)
        {

            try
            {
                if (isActive)
                {
                    return await _context.Users
                        .Where(u => u.IsActive == isActive)
                        .FirstOrDefaultAsync(u => u.Phone == phone);
                }
                return await _context.Users
                    .FirstOrDefaultAsync(u => u.Phone == phone);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<User?> GetUserByIdAsync(long id)
        {
            try
            {
                return await _context.Users
                    .Include(c => c.Point)
                    .Include(c => c.Wallet)
                    .Include(c => c.TutorProfile)
                    .ThenInclude(t => t.Certifications)
                    .SingleOrDefaultAsync(c => c.UserId == id);
                    
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
