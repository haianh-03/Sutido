using Sutido.Model.Enums;

namespace ViewModels.Responses
{
    public class AuthResponse
    {
        public long UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public RoleType Role { get; set; }
        public string Token { get; set; } = null!; // ⬅️ Bỏ dấu comment
    }
}