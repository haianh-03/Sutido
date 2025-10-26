using Microsoft.AspNetCore.Http;
namespace Sutido.API.ViewModels.Requests
{
    public class CertificationAddRequest
    {
        public long TutorProfileId { get; set; }

        public string DocumentType { get; set; } = null!;

        public string? Note { get; set; }

        public IFormFile File { get; set; } = null!;

        
    }
}
