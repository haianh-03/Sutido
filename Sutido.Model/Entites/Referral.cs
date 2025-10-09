using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class Referral
{
    public long ReferralId { get; set; }

    public long ReferrerUserId { get; set; }

    public long? ReferredUserId { get; set; }

    public string? ReferredEmail { get; set; }

    public int RewardPoints { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<PointTransaction> PointTransactions { get; set; } = new List<PointTransaction>();

    public virtual User? ReferredUser { get; set; }

    public virtual User ReferrerUser { get; set; } = null!;
}
