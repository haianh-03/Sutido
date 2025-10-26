using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sutido.API.ViewModels.Requests;
using Sutido.API.ViewModels.Responses;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;

namespace Sutido.API.Controllers
{
    [Route("/api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _iUserService;
        private readonly IMapper _iMapper;

        public UserController(IUserService iUserService, IMapper iMapper)
        {
            _iMapper = iMapper;
            _iUserService = iUserService;
        }

        // get all user - staff/admin
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] string? keyword,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortOrder = "asc",
            [FromQuery] string? role = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
            )
        {
            var filter = role == null ? null : new Dictionary<string, string> { { "Role", role } };
            var list = await _iUserService.GetAllAsync(keyword, sortBy, sortOrder, page, pageSize, filter);

            if (list == null || list.Count() == 0) return NotFound();

            //var result 

            return Ok(list);
        }

        // get profile - customer
        [HttpGet("{id}/profile")]
        public async Task<IActionResult> GetProfileAsync(long id)
        {
            var profile = await _iUserService.GetByIdAsync(id);

            if (profile == null) return NotFound();

            var result = _iMapper.Map<ProfileResponse>(profile);

            return Ok(result);
        }

        // get user detail
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserByIdAsync(long id)
        {
            var response = await _iUserService.GetUserByIdAsync(id);
            if (response == null) return NotFound();
            return Ok(response);
        }


        // add user - admin
        [HttpPost]
        public async Task<IActionResult> AddStaffAsync([FromBody] StaffRequest c)
        {
            var user = _iMapper.Map<User>(c);

            var result = await _iUserService.AddAsync(user);

            if (result <= 0) return StatusCode(500, "Failed to create user.");

            return Ok();
        }


        // Update profile
        [HttpPut("{id}")]
        public  async Task<IActionResult> UpdateProfileAsync(long id, [FromBody] ProfileRequest profile)
        {
            var user = _iMapper.Map<User>(profile);

            var result = await _iUserService.UpdateAsync(id, user);

            if (result <= 0) return StatusCode(500, "Failed to update user");

            return Ok();
        }

        // Delete user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var result = await _iUserService.SoftDeleteUserAsync(id);

            if (result <= 0) return StatusCode(500, "Failed to delete user");

            return NoContent();
        }
    }
}
