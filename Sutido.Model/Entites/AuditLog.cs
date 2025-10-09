using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class AuditLog
{
    public long AuditId { get; set; }

    public string Entity { get; set; } = null!;

    public string EntityId { get; set; } = null!;

    public string Action { get; set; } = null!;

    public long PerformedByUserId { get; set; }

    public DateTimeOffset PerformedAt { get; set; }

    public string? Details { get; set; }

    public virtual User PerformedByUser { get; set; } = null!;
}
