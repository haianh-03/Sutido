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
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public ReviewController(IReviewService reviewService, IBookingService bookingService, IMapper mapper)
        {
            _reviewService = reviewService;
            _bookingService = bookingService;
            _mapper = mapper;
        }

        // ... (Các hàm Get không đổi) ...

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReviewsForUser(long userId)
        {
            var reviews = await _reviewService.GetReviewsForUserAsync(userId);
            var response = _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            if (review == null) return NotFound();

            var response = _mapper.Map<ReviewResponse>(review);
            return Ok(response);
        }

        // ===================================
        // ⭐ 3. Tạo review mới (Đã sửa)
        // ===================================
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewCreateRequest request)
        {
            var senderId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // 2. Lấy booking (VÀ CHATROOM CỦA NÓ)
            var booking = await _bookingService.GetByIdAsync(request.BookingId);
            if (booking == null) return NotFound("Không tìm thấy booking.");

            // ⚠️ Đảm bảo service của bạn đã .Include(b => b.ChatRoom)
            if (booking.ChatRoom == null)
            {
                return BadRequest("Lỗi: Booking service chưa tải thông tin ChatRoom.");
            }

            // 3. Logic xác định người nhận review (Đã sửa)
            long toUserId;
            if (senderId == booking.ChatRoom.ParentUserId) // ⬅️ SỬA LỖI 1
            {
                toUserId = booking.ChatRoom.TutorUserId; // ⬅️ SỬA LỖI 1
            }
            else if (senderId == booking.ChatRoom.TutorUserId) // ⬅️ SỬA LỖI 1
            {
                toUserId = booking.ChatRoom.ParentUserId; // ⬅️ SỬA LỖI 1
            }
            else
            {
                return Forbid("Bạn không phải là thành viên của booking này.");
            }

            var review = new Review
            {
                BookingId = request.BookingId,
                Rating = request.Rating,
                Comment = request.Comment,
                FromUserId = senderId,
                ToUserId = toUserId,
                CreatedAt = DateTimeOffset.UtcNow
            };

            await _reviewService.AddAsync(review);

            // Cần tải lại review với đủ thông tin user để map
            var createdReviewWithInfo = await _reviewService.GetByIdAsync(review.ReviewId);
            var response = _mapper.Map<ReviewResponse>(createdReviewWithInfo);

            return CreatedAtAction(nameof(GetById), new { id = response.ReviewId }, response);
        }

        // ===================================
        // 🛠️ 4. Cập nhật review (Không đổi)
        // ===================================
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(long id, [FromBody] ReviewUpdateRequest request)
        {
            var senderId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var existingReview = await _reviewService.GetByIdAsync(id);

            if (existingReview == null) return NotFound();
            if (existingReview.FromUserId != senderId)
            {
                return Forbid("Bạn không có quyền sửa review này.");
            }

            existingReview.Rating = request.Rating;
            existingReview.Comment = request.Comment;
            await _reviewService.UpdateAsync(existingReview);

            return NoContent();
        }

        // ===================================
        // ❌ 5. Xóa review (Đã sửa)
        // ===================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(long id)
        {
            var senderId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var existingReview = await _reviewService.GetByIdAsync(id);

            if (existingReview == null) return NotFound();
            if (existingReview.FromUserId != senderId)
            {
                return Forbid("Bạn không có quyền xóa review này.");
            }

            // ⬇️ SỬA LỖI 3 ⬇️
            await _reviewService.DeleteAsync(existingReview);

            return NoContent();
        }
    }
}