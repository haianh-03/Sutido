using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class PointTransaction
{
    public long PointTransactionId { get; set; }

    public long PointId { get; set; }

    public long? ReferralId { get; set; }

    public int ChangeAmount { get; set; }

    public string? Reason { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public virtual Point Point { get; set; } = null!;

    public virtual Referral? Referral { get; set; }
}
