using Sutido.Model.Enums;

namespace Sutido.API.ViewModels.Responses
{
    public class ProfileResponse
    {
        public long UserId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Street { get; set; }

        public string? Ward { get; set; }

        public string? District { get; set; }

        public string? City { get; set; }

        public RoleType Role { get; set; }

        public long PointId { get; set; }

        public int TotalPoint { get; set; }
    }
}
