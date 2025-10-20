using Microsoft.AspNetCore.Mvc;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;

namespace Sutido.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _bookingService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            return booking == null ? NotFound() : Ok(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {
            await _bookingService.CreateAsync(booking);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Booking booking)
        {
            if (id != booking.BookingId) return BadRequest();
            await _bookingService.UpdateAsync(booking);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _bookingService.DeleteAsync(id);
            return Ok();
        }
    }
}
