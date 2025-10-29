using System.ComponentModel.DataAnnotations;

namespace Sutido.API.ViewModels.Requests
{
    public class MessageCreateTextRequest
    {
        [Required]
        public long ChatRoomId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; } = null!;

        // ❌ Lưu ý: SenderId không có ở đây.
        // Chúng ta sẽ lấy nó từ token của người dùng đã đăng nhập.
    }
}