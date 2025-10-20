using System;

namespace Sutido.Model.Entites
{
    public partial class Message
    {
        public long MessageId { get; set; }
        public long ChatRoomId { get; set; }
        public long SenderId { get; set; }
        public string? Content { get; set; }
        public string MessageType { get; set; } = "text";
        public string? FileUrl { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTimeOffset SentAt { get; set; } = DateTimeOffset.UtcNow;
        public virtual ChatRoom ChatRoom { get; set; } = null!;
        public virtual User Sender { get; set; } = null!;
    }
}
