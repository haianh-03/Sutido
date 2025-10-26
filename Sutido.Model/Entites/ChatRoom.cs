namespace Sutido.Model.Entites;

public partial class ChatRoom
{
    public long ChatRoomId { get; set; }

    public long ParentPostId { get; set; }

    public long TutorPostId { get; set; }

    public long ParentUserId { get; set; }

    public long TutorUserId { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset? ExpiresAt { get; set; }

    public bool ParentDeposited { get; set; }

    public bool TutorDeposited { get; set; }

    public long? ParentDepositTransactionId { get; set; }

    public long? TutorDepositTransactionId { get; set; }

    public bool IsActive { get; set; }

    public bool IsConfirmed { get; set; }

    public DateTimeOffset? ConfirmedAt { get; set; }

    public bool RuleAcceptedByParent { get; set; }

    public bool RuleAcceptedByTutor { get; set; }

    public string? CancelReason { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Post ParentPost { get; set; } = null!;

    public virtual User ParentUser { get; set; } = null!;

    public virtual Post TutorPost { get; set; } = null!;

    public virtual User TutorUser { get; set; } = null!;
    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

}
