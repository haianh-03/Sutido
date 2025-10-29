namespace Sutido.API.ViewModels.Responses
{
    public class TrackingResponse
    {
        public long TrackingId { get; set; }
        public long BookingId { get; set; }

        // Thêm thông tin Tutor
        public long TutorUserId { get; set; }
        public string TutorUserName { get; set; } = null!; // Lấy từ TutorUser.FullName

        public string Action { get; set; } = null!;
        public DateTimeOffset ActionAt { get; set; }
        public string? Location { get; set; }
        public string? SecurityCodeUsed { get; set; }
    }
}