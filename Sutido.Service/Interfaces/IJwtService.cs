using Sutido.Model.Entites;

namespace Sutido.Service.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
