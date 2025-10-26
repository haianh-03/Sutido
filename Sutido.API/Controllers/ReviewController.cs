using Microsoft.AspNetCore.Mvc;
using Sutido.Model;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;
using System.Threading.Tasks;

namespace Sutido.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var review = await _service.GetByIdAsync(id);
            return review == null ? NotFound() : Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Review review)
        {
            await _service.AddAsync(review);
            return Ok(review);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Review review)
        {
            if (id != review.ReviewId) return BadRequest();
            await _service.UpdateAsync(review);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
