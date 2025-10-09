using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class MarketplaceItem
{
    public long ItemId { get; set; }

    public long SellerUserId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? FileUrl { get; set; }

    public bool IsPublished { get; set; }

    public bool PushPriority { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public virtual User SellerUser { get; set; } = null!;
}
