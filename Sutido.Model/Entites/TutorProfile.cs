using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class TutorProfile
{
    public long TutorProfileId { get; set; }

    public long UserId { get; set; }

    public string? Bio { get; set; }

    public string? Education { get; set; }

    public int ExperienceYears { get; set; }

    public bool IsApproved { get; set; }

    public DateTimeOffset? ApprovedAt { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
