using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sutido.Model;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;

namespace SutidoWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SutidoProjectContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(SutidoProjectContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized("Email không tồn tại");

            // ⚠️ Ở đây PasswordHash đang lưu plaintext hoặc hash. 
            // Nếu bạn dùng BCrypt, cần xác minh bằng BCrypt.Verify
            if (user.PasswordHash != request.Password)
                return Unauthorized("Mật khẩu không đúng");

            if (!user.IsActive)
                return Unauthorized("Tài khoản bị vô hiệu hóa");

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                user.UserId,
                user.Email,
                user.FullName,
                user.RoleId
            });
        }
    }

    // DTO
    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
