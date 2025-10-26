using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Sutido.API.ViewModels.Requests
{
    public class CertificationRequest
    {
        [Required]
        public string DocumentType { get; set; } = null!;

        public string? Note { get; set; }

        public IFormFile File { get; set; } = null!;
    }
}
