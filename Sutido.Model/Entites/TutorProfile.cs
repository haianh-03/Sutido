using Sutido.Model.Enums;

namespace Sutido.Model.Entites;

public partial class TutorProfile
{
    public long TutorProfileId { get; set; }

    public long UserId { get; set; }

    public long? ReviewerBy { get; set; }

    public string? Description { get; set; }

    public EducationLevel Education { get; set; }

    public int ExperienceYears { get; set; }

    public DateTimeOffset? ReviewedAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public StatusType Status { get; set; } = StatusType.Pending;

    public string? Reason { get; set; }

    // User
    public virtual User User { get; set; } = null!; // người dùng

    public virtual User? ReviewedByNavigation { get; set; } // người duyệt

    // Certification
    public virtual ICollection<Certification> Certifications { get; set; } = new List<Certification>();
}
