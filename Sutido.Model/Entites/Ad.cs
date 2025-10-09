using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class Ad
{
    public long AdId { get; set; }

    public string AdvertiserName { get; set; } = null!;

    public string AdType { get; set; } = null!;

    public string? ContentUrl { get; set; }

    public DateTimeOffset StartAt { get; set; }

    public DateTimeOffset EndAt { get; set; }

    public bool IsActive { get; set; }

    public DateTimeOffset CreatedAt { get; set; }
}
