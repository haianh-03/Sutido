using System.ComponentModel.DataAnnotations;

namespace Sutido.API.ViewModels.Requests
{
    public class ReviewCreateRequest
    {
        [Required]
        public long BookingId { get; set; }

        [Required]
        [Range(1, 5)] // Đảm bảo rating là từ 1 đến 5 sao
        public int Rating { get; set; }

        public string? Comment { get; set; }

        // ❌ KHÔNG CÓ FromUserId hay ToUserId ở đây.
        // Server sẽ tự động xử lý:
        // 1. FromUserId = Lấy từ token của người đang đăng nhập.
        // 2. ToUserId = Lấy từ BookingId (nếu người gửi là Parent, ToUserId là Tutor, và ngược lại).
    }
}