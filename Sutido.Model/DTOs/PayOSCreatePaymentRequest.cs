using System.ComponentModel.DataAnnotations;

namespace Sutido.Model.Entites
{
    public class PayOSCreatePaymentRequest
    {
        [Required]
        public long OrderCode { get; set; }  // ⚠️ PayOS yêu cầu là số

        [Required]
        public int Amount { get; set; }  // VNĐ

        [Required]
        public string Description { get; set; } = string.Empty;

        public string CancelUrl { get; set; } = "https://sutido.vn/payment/cancel";
        public string ReturnUrl { get; set; } = "https://sutido.vn/payment/success";

        // ⚙️ Thêm thông tin người mua (optional nhưng nên có)
        public string BuyerName { get; set; } = "Phụ huynh Sutido";
        public string BuyerEmail { get; set; } = "parent@sutido.vn";
    }
}
