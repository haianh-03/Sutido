using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class Review
{
    public long ReviewId { get; set; }

    public long BookingId { get; set; }

    public long FromUserId { get; set; }

    public long ToUserId { get; set; }

    public int Rating { get; set; }

    public string? Comment { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual User FromUser { get; set; } = null!;

    public virtual User ToUser { get; set; } = null!;
}
