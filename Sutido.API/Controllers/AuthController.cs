using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sutido.API.ViewModels.Requests;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;

namespace Sutido.API.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _iService;
        private readonly IMapper _iMapper;

        public AuthController(IAuthService iService, IMapper iMapper)
        {
            _iMapper = iMapper;
            _iService = iService;
        }

        // register - customer
        [HttpPost("register")]
        public async Task<IActionResult> AddAsync([FromBody] CustomerRequest c)
        {
            if (c.Password != c.ConfirmPassword)
                return BadRequest("Passwords do not match.");

            var user = _iMapper.Map<User>(c);
            user.PasswordHash = c.Password;

            var response = await _iService.RegisterAsync(user);

            if (response == null) return StatusCode(500, "Failed to create user.");

            return Ok(response);
        }

        // login - cus/staff/admin
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var response = await _iService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        // logout - cus/staff/admin
    }
}
