namespace Sutido.API.ViewModels.Requests
{
    public class ProfileRequest
    {
        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Street { get; set; }

        public string? Ward { get; set; }

        public string? District { get; set; }

        public string? City { get; set; }
    }
}
