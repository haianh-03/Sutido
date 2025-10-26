using Sutido.API.ViewModels.Requests;
using Sutido.Model.Entites;
using ViewModels.Responses;

namespace Sutido.Service.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(User u);
        Task<AuthResponse?> LoginAsync(LoginRequest request);
    }
}
