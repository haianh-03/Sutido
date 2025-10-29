using System.ComponentModel.DataAnnotations;

namespace Sutido.API.ViewModels.Requests
{
    public class TrackingCreateRequest
    {
        [Required]
        public long BookingId { get; set; }

        [Required]
        public string Action { get; set; } = null!; // Ví dụ: "Arrived", "StartedSession", "UsedCode"

        public string? Location { get; set; } // Ví dụ: tọa độ GPS

        public string? SecurityCodeUsed { get; set; } // Mã bảo mật nếu hành động là "kết thúc"

        // ❌ KHÔNG CÓ TutorUserId (lấy từ token)
        // ❌ KHÔNG CÓ ActionAt (lấy từ server)
    }
}