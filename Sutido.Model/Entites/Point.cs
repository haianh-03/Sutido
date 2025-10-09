using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class Point
{
    public long PointId { get; set; }

    public long UserId { get; set; }

    public int Points { get; set; }

    public string? Reason { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public virtual ICollection<PointTransaction> PointTransactions { get; set; } = new List<PointTransaction>();

    public virtual User User { get; set; } = null!;
}
