using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class Verification
{
    public long VerificationId { get; set; }

    public long UserId { get; set; }

    public string DocumentType { get; set; } = null!;

    public string FileUrl { get; set; } = null!;

    public DateTimeOffset SubmittedAt { get; set; }

    public long? ReviewedBy { get; set; }

    public DateTimeOffset? ReviewedAt { get; set; }

    public string Status { get; set; } = null!;

    public string? Note { get; set; }

    public virtual User? ReviewedByNavigation { get; set; }

    public virtual User User { get; set; } = null!;
}
