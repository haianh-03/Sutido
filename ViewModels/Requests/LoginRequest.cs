using System.ComponentModel.DataAnnotations;

namespace Sutido.API.ViewModels.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Vui lòng nhập email hoặc số điện thoại.")]
        public string Identifier { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        public string Password { get; set; } = null!;
    }
}
