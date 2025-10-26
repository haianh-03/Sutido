using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sutido.API.ViewModels.Requests;
using Sutido.API.ViewModels.Responses;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;

namespace Sutido.API.Controllers
{
    [Route("/api/certifications")]
    [ApiController]
    public class CertificationController : Controller
    {
        private readonly ICertificationService _iService;
        private readonly IMapper _iMapper;

        public CertificationController(ICertificationService iService, IMapper iMapper)
        {
            _iService = iService;
            _iMapper = iMapper;
        }

        // get all certification by tutor profile id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync(long id)
        {
            var list = await _iService.GetAllAsync(id);

            if (list == null || list.Count() == 0) return NotFound();

            var response = _iMapper.Map<IEnumerable<CertificationResponse>>(list);

            return Ok(response);
        }

        // add certification
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadAndAddAsync([FromForm] CertificationAddRequest c)
        {
            var result = await _iService.UploadAndAddAsync(c.TutorProfileId, c.DocumentType, c.Note, c.File);

            if (result <= 0) return BadRequest("Failed to add certification.");

            return Ok("Add succesfully.");
        }

        // delete certification
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await _iService.DeleteAsync(id);

            if (result <= 0) return BadRequest("Failed to delete certification.");

            return NoContent();
        }

        // review tutor profile - admin/staff
        [HttpPut("review")]
        public async Task<IActionResult> ReviewTutorProfile([FromBody] CertificationReviewRequest c)
        {
            var cert = _iMapper.Map<Certification>(c);

            var result = await _iService.ReviewCertificationAsync(cert);

            if (result <= 0) return BadRequest("Failed to review tutor profile.");

            return Ok();
        }
    }
}
