using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class Tracking
{
    public long TrackingId { get; set; }

    public long BookingId { get; set; }

    public long TutorUserId { get; set; }

    public string Action { get; set; } = null!;

    public DateTimeOffset ActionAt { get; set; }

    public string? Location { get; set; }

    public string? SecurityCodeUsed { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual User TutorUser { get; set; } = null!;
}
