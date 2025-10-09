using System;
using System.Collections.Generic;

namespace Sutido.Model.Entites;

public partial class Booking
{
    public long BookingId { get; set; }

    public long ChatRoomId { get; set; }

    public decimal AgreedPricePerSession { get; set; }

    public int SessionsPerWeek { get; set; }

    public string AgreedDays { get; set; } = null!;

    public string AgreedTime { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string SecurityCode { get; set; } = null!;

    public string BookingStatus { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; }

    public virtual ChatRoom ChatRoom { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Tracking> Trackings { get; set; } = new List<Tracking>();
}
