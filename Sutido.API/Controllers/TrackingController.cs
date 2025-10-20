using Microsoft.AspNetCore.Mvc;
using Sutido.Model;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;
using System.Threading.Tasks;

namespace Sutido.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _service;

        public TrackingController(ITrackingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tracking = await _service.GetByIdAsync(id);
            return tracking == null ? NotFound() : Ok(tracking);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Tracking tracking)
        {
            await _service.AddAsync(tracking);
            return Ok(tracking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tracking tracking)
        {
            if (id != tracking.TrackingId) return BadRequest();
            await _service.UpdateAsync(tracking);
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
