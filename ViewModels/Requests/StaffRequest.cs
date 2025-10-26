namespace Sutido.API.ViewModels.Requests
{
    public class StaffRequest
    {
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string Password { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public DateTimeOffset DateOfBirth { get; set; }

        public string? Street { get; set; }

        public string? Ward { get; set; }

        public string? District { get; set; }

        public string? City { get; set; }
    }
}
