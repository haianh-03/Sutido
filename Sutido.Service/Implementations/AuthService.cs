using Sutido.API.ViewModels.Requests;
using Sutido.Model.Entites;
using Sutido.Model.Enums;
using Sutido.Repository.Interfaces;
using Sutido.Repository.UnitOfWork;
using Sutido.Service.Interfaces;
using ViewModels.Responses;

namespace Sutido.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _iRepo;

        public AuthService(IUnitOfWork iRepo)
        {
            _iRepo = iRepo;
        }

        public async Task<AuthResponse> RegisterAsync(User u)
        {
            var user = await _iRepo.Users.FindUserByEmailAsync(u.Email, false);
            if (user != null) throw new Exception("Email already exists.");

            u.Role = RoleType.Customer;
            u.PasswordHash = BCrypt.Net.BCrypt.HashPassword(u.PasswordHash);
            u.Point = new Point { TotalPoint = 0, CreatedAt = DateTimeOffset.UtcNow };
            u.Wallet = new Wallet { Balance = 0, CreatedAt = DateTimeOffset.UtcNow };

            await _iRepo.Users.AddAsync(u);
            await _iRepo.SaveAsync();

            return new AuthResponse
            {
                UserId = u.UserId,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                //Token = token
            };
        }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            User? user = null;
            if (request.Identifier.Contains("@"))
            {
                user = await _iRepo.Users.FindUserByEmailAsync(request.Identifier, true);
            }
            else if (request.Identifier.StartsWith("0"))
            {
                user = await _iRepo.Users.FindUserByPhoneAsync(request.Identifier, true);
            }
            if (user == null)
                throw new Exception("Invalid email or password.");

            bool isValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            if (!isValid)
                throw new Exception("Invalid email or password.");

            return new AuthResponse
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                //Token = token
            };
        }
    }
}
