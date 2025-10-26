using Microsoft.AspNetCore.Http;
using Sutido.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sutido.API.ViewModels.Requests
{
    public class TutorProfileRequest
    {
        [Required]
        public long UserId { get; set; }

        public string? Description { get; set; }

        [Required]
        public EducationLevel Education { get; set; }

        [Required]
        public int ExperienceYears { get; set; }

        public List<string> Docs { get; set; } = new();

        public List<string> Notes { get; set; } = new();
        
        public List<IFormFile> Files { get; set; } = new();

    }
}
