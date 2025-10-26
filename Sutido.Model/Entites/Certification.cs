using Sutido.Model.Enums;

namespace Sutido.Model.Entites;

public partial class Certification
{
    public long CertificationId { get; set; }

    public long TutorProfileId { get; set; }

    public string DocumentType { get; set; } = null!;

    public string FileUrl { get; set; } = null!;

    public DateTimeOffset SubmittedAt { get; set; } = DateTimeOffset.UtcNow;

    public string? Note { get; set; }

    public long? ReviewedBy { get; set; }

    public DateTimeOffset? ReviewedAt { get; set; } = DateTimeOffset.UtcNow;

    public StatusType Status { get; set; } = StatusType.Pending;

    // User
    public virtual User? ReviewedByNavigation { get; set; } // người duyệt

    // TutorProfile
    public virtual TutorProfile TutorProfile { get; set; } = null!;
}
