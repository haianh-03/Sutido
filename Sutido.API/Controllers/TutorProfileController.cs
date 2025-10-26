using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sutido.API.ViewModels.Requests;
using Sutido.API.ViewModels.Responses;
using Sutido.Model.Entites;
using Sutido.Model.Enums;
using Sutido.Service.Interfaces;


namespace Sutido.API.Controllers
{
    [Route("/api/tutor-profiles")]
    [ApiController]
    public class TutorProfileController : Controller
    {
        private readonly ITutorProfileService _iService;
        private readonly IMapper _iMapper;

        public TutorProfileController(ITutorProfileService iService, IMapper iMapper)
        {
            _iMapper = iMapper;
            _iService = iService;
        }

        // register tutor profile - customer
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> RegisterTutorProfile([FromForm] TutorProfileRequest t)
        {
            var tutor = _iMapper.Map<TutorProfile>(t);

            var result = await _iService.UploadAndAddAsync(tutor, t.Docs, t.Notes, t.Files);

            if (result <= 0) return StatusCode(500, "Failed to create tutor profile.");

            return Ok();
        }

        // review tutor profile - admin/staff
        [HttpPut("review")]
        public async Task<IActionResult> ReviewTutorProfile([FromBody] TutorProfileReviewRequest t)
        {
            var profile = _iMapper.Map<TutorProfile>(t);

            var result = await _iService.ReviewTutorProfileAsync(profile);

            if (result <= 0) return BadRequest("Failed to review tutor profile.");

            return Ok();
        }

        // get all tutor profile - admin/staff
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] string? sortBy,
            [FromQuery] string? sortOrder = "asc",
            [FromQuery] StatusType? status = null,
            [FromQuery] EducationLevel? edu = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var filters = new Dictionary<string, string>();
            if (status != null)
                filters.Add("status", status.ToString() ?? "");
            if (edu != null)
                filters.Add("Education", edu.ToString() ?? "");
            var list = await _iService.GetAllAsync(sortBy, sortOrder, page, pageSize, filters);
            if (list == null || list.Count() == 0) return NotFound();

            var response = _iMapper.Map<IEnumerable<TutorProfileResponse>>(list);
            return Ok(response);
        }

        // get tutor profile by tutor profile id - tutor
        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfileByIdAsync(long id)
        {
            var profile = await _iService.GetProfileByIdAsync(id, false);

            if (profile == null) return NotFound();

            var response = _iMapper.Map<TutorProfileResponse>(profile);

            return Ok(response);
        }

        // get tutor profile by tutor profile id - admin/staff
        [HttpGet("profile/{id}/review")]
        public async Task<IActionResult> GetProfileByIdReviewAsync(long id)
        {
            var profile = await _iService.GetProfileByIdAsync(id, true);

            if (profile == null) return NotFound();

            var response = _iMapper.Map<TutorProfileResponse>(profile);

            return Ok(response);
        }

        // get tutor profile by user id - tutor
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetProfileByUserIdAsync(long userId)
        {
            var profile = await _iService.GetProfileByUserIdAsync(userId);

            if (profile == null) return NotFound();

            var response = _iMapper.Map<TutorProfileResponse>(profile);

            return Ok(response);
        }

        // update tutor profile - tutor
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] TutorProfileUpdateRequest t)
        {
            var profile = _iMapper.Map<TutorProfile>(t);

            var result = await _iService.UpdateAsync(id, profile);

            if (result <= 0) return StatusCode(500, "Failed to update tutor profile.");

            return Ok("Update successfully!");
        }
    }
}
