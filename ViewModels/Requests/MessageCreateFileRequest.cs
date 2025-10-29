using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Sutido.API.ViewModels.Requests
{
    //Dùng khi gửi file/hình ảnh, sử dụng [FromForm]
    public class MessageCreateFileRequest
    {
        [Required]
        public long ChatRoomId { get; set; }

        [Required]
        public IFormFile File { get; set; } = null!;

        // Có thể dùng để gửi caption kèm theo file
        public string? Content { get; set; }
    }
}