using System.ComponentModel.DataAnnotations;

namespace Sutido.API.ViewModels.Requests
{
    public class CustomerRequest
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public DateTimeOffset DateOfBirth { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Phone]
        public string? Phone { get; set; }
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}
