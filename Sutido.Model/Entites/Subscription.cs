using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class Subscription
{
    public long SubscriptionId { get; set; }

    public long UserId { get; set; }

    public string PackageName { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTimeOffset StartAt { get; set; }

    public DateTimeOffset EndAt { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
