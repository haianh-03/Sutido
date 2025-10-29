using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sutido.API.ViewModels.Requests;
using Sutido.API.ViewModels.Responses;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;

namespace Sutido.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _bookingService.GetAllAsync();
            var responses = _mapper.Map<IEnumerable<BookingResponse>>(bookings);
            return Ok(responses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var booking = await _bookingService.GetByIdAsync(id);
            if (booking == null) return NotFound();
            return Ok(_mapper.Map<BookingResponse>(booking));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var booking = _mapper.Map<Booking>(request);
            await _bookingService.CreateAsync(booking);

            var response = _mapper.Map<BookingResponse>(booking);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] BookingRequest request)
        {
            var existing = await _bookingService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(request, existing);
            await _bookingService.UpdateAsync(existing);

            return Ok(_mapper.Map<BookingResponse>(existing));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _bookingService.DeleteAsync(id);
            return Ok();
        }
    }
}
