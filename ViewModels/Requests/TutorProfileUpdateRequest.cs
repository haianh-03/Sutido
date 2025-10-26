using Sutido.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace Sutido.API.ViewModels.Requests
{
    public class TutorProfileUpdateRequest
    {
        public string? Description { get; set; }

        [Required]
        public EducationLevel Education { get; set; }

        [Required]
        public int ExperienceYears { get; set; }
    }
}
