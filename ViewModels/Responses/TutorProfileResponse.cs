using Sutido.Model.Enums;

namespace Sutido.API.ViewModels.Responses
{
    public class TutorProfileResponse
    {
        public long TutorProfileId { get; set; }

        public long UserId { get; set; }

        public long? ReviewerBy { get; set; }

        public string? Description { get; set; }

        public EducationLevel Education { get; set; }

        public int ExperienceYears { get; set; }

        public DateTimeOffset? ReviewedAt { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public StatusType Status { get; set; }
        
        public string? Reason { get; set; }

        public List<CertificationResponse> Certifications { get; set; } = new();
    }
}
