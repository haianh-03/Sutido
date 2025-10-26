using Sutido.Model.Entites;
using Sutido.Model.Enums;
using Sutido.Repository.Implementations;
using Sutido.Repository.UnitOfWork;
using Sutido.Service.Interfaces;


namespace Sutido.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> GetAllAsync(string? keyword, string? sortBy, string? sortOrder, int page, int pageSize, Dictionary<string, string>? filters)
        {
            return await _unitOfWork.Users.GetAllAsync(keyword, sortBy, sortOrder, page, pageSize, filters);
        }
        public async Task<User?> GetByIdAsync(long id)
        {
            return await _unitOfWork.Users.GetProfileAsync(id);
        }

        public async Task<User?> GetUserByIdAsync(long id)
        {
            return await _unitOfWork.Users.GetUserByIdAsync(id);
        }

        public async Task<int> AddAsync(User u)
        {
            var user = await _unitOfWork.Users.FindUserByEmailAsync(u.Email, false);
            if (user != null) throw new InvalidOperationException("Email already exists.");
            u.Role = RoleType.Staff;
            u.PasswordHash = BCrypt.Net.BCrypt.HashPassword(u.PasswordHash);
            await _unitOfWork.Users.AddAsync(u);
            var result = await _unitOfWork.SaveAsync();
            return result;
        }

        public async Task<int> UpdateAsync(long id, User u)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user == null) throw new InvalidOperationException("Default user not found.");

            if (!string.IsNullOrEmpty(u.Email))
            {
                var emailUser = await _unitOfWork.Users.FindUserByEmailAsync(u.Email, false);
                if (emailUser != null && emailUser.UserId != id)
                    throw new InvalidOperationException("Email already exists.");
                user.Email = u.Email;
            }

            if (!string.IsNullOrEmpty(u.Phone))
            {
                var phoneUser = await _unitOfWork.Users.FindUserByPhoneAsync(u.Phone, false);
                if (phoneUser != null && phoneUser.UserId != id)
                    throw new InvalidOperationException("Phone already exists.");
                user.Phone = u.Phone;
            }

            user.Street = u.Street ?? user.Street;
            user.Ward = u.Ward ?? user.Ward;
            user.District = u.District ?? user.District;
            user.City = u.City ?? user.City;
            user.UpdatedAt = DateTimeOffset.UtcNow;

            _unitOfWork.Users.Update(user);
            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> SoftDeleteUserAsync(long id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found");

            user.IsActive = false;
            user.UpdatedAt = DateTimeOffset.UtcNow;

            _unitOfWork.Users.Update(user);
            return await _unitOfWork.SaveAsync();
        }
    }
}
