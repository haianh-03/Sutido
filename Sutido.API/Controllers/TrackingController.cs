using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sutido.API.ViewModels.Requests;
using Sutido.API.ViewModels.Responses;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;
using System.Security.Claims;

namespace Sutido.API.Controllers
{
    [Authorize] // 🔒 BẮT BUỘC: Chỉ người đăng nhập mới được tracking
    [ApiController]
    [Route("api/[controller]")]
    public class TrackingController : ControllerBase
    {
        private readonly ITrackingService _service;
        private readonly IMapper _mapper;

        public TrackingController(ITrackingService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // ===================================
        // 💬 1. Lấy lịch sử tracking của 1 booking
        // ===================================
        [HttpGet("booking/{bookingId}")]
        public async Task<IActionResult> GetByBookingId(long bookingId)
        {
            // TODO: Nên kiểm tra xem user hiện tại (Parent/Tutor)
            // có quyền xem bookingId này không.

            var trackingLogs = await _service.GetByBookingIdAsync(bookingId);
            var response = _mapper.Map<IEnumerable<TrackingResponse>>(trackingLogs);
            return Ok(response);
        }

        // ===================================
        // 💬 2. Lấy 1 tracking log theo ID
        // ===================================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id) // <-- Sửa sang long
        {
            var tracking = await _service.GetByIdAsync(id);
            if (tracking == null) return NotFound();

            var response = _mapper.Map<TrackingResponse>(tracking);
            return Ok(response);
        }

        // ===================================
        // 📍 3. Thêm một tracking log MỚI
        // ===================================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TrackingCreateRequest request)
        {
            // Lấy ID của Tutor từ token (không tin client)
            var tutorId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // TODO: Kiểm tra xem Tutor này có phải là Tutor của BookingId không

            var tracking = new Tracking
            {
                BookingId = request.BookingId,
                Action = request.Action,
                Location = request.Location,
                SecurityCodeUsed = request.SecurityCodeUsed,

                TutorUserId = tutorId, // ⬅️ Gán ID an toàn
                ActionAt = DateTimeOffset.UtcNow // ⬅️ Gán thời gian an toàn
            };

            await _service.AddAsync(tracking);

            // Tải lại để lấy thông tin User.FullName cho response
            var newTracking = await _service.GetByIdAsync(tracking.TrackingId);
            var response = _mapper.Map<TrackingResponse>(newTracking);

            return CreatedAtAction(nameof(GetById), new { id = response.TrackingId }, response);
        }

        // ===================================
        // ❌ 4. ĐÃ XÓA UPDATE VÀ DELETE
        // ===================================
        // [HttpPut] và [HttpDelete] đã bị xóa
        // vì chúng vi phạm logic của một hệ thống log.
    }
}