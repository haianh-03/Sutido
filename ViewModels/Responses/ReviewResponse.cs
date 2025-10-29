namespace Sutido.API.ViewModels.Responses
{
    public class ReviewResponse
    {
        public long ReviewId { get; set; }
        public long BookingId { get; set; }

        // Thông tin người viết
        public long FromUserId { get; set; }
        public string FromUserName { get; set; } = null!; // Lấy từ User.FullName

        // Thông tin người được review
        public long ToUserId { get; set; }
        public string ToUserName { get; set; } = null!; // Lấy từ User.FullName

        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}