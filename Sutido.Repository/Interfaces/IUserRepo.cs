using Sutido.Model.Entites;
using Sutido.Repository.Generic;

namespace Sutido.Repository.Interfaces
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User?> GetProfileAsync(long id);
        Task<User?> FindUserByEmailAsync(string email, bool isActive);
        Task<User?> FindUserByPhoneAsync(string email, bool isActive);
        Task<User?> GetUserByIdAsync(long id);
    }
}
