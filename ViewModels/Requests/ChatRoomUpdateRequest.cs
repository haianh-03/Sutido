namespace Sutido.API.ViewModels.Requests
{
    public class ChatRoomUpdateRequest
    {
        // Chỉ cho phép cập nhật các trường trạng thái
        // Không cho phép cập nhật các ID (ParentPostId, TutorUserId, v.v.)
        public bool IsActive { get; set; }
        public bool IsConfirmed { get; set; }
        public bool RuleAcceptedByParent { get; set; }
        public bool RuleAcceptedByTutor { get; set; }
        public string? CancelReason { get; set; }
        public DateTimeOffset? ExpiresAt { get; set; }
    }
}