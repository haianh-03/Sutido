namespace Sutido.API.ViewModels.Responses
{
    public class ChatRoomResponse
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
    }
}