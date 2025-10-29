namespace Sutido.API.ViewModels.Responses
{
    public class MessageResponse
    {
        public long MessageId { get; set; }
        public long ChatRoomId { get; set; }
        public long SenderId { get; set; }

        // Chúng ta nên thêm tên người gửi
        // để client không phải gọi API khác để lấy tên
        public string SenderName { get; set; } = null!;

        public string? Content { get; set; }
        public string MessageType { get; set; } = "text";
        public string? FileUrl { get; set; }
        public bool IsRead { get; set; }
        public DateTimeOffset SentAt { get; set; }
    }
}