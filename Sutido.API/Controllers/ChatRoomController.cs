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
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IMapper _mapper; // <-- Thêm AutoMapper

        public ChatRoomController(IChatRoomService chatRoomService, IMapper mapper)
        {
            _chatRoomService = chatRoomService;
            _mapper = mapper; // <-- Thêm AutoMapper
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var chatRooms = await _chatRoomService.GetAllAsync();
            var response = _mapper.Map<IEnumerable<ChatRoomResponse>>(chatRooms);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var chatRoom = await _chatRoomService.GetByIdAsync(id);
            if (chatRoom == null) return NotFound();

            var response = _mapper.Map<ChatRoomResponse>(chatRoom);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ChatRoomCreateRequest request)
        {
            // Map từ Request DTO sang Entity
            var chatRoom = _mapper.Map<ChatRoom>(request);

            // Các logic khác (set IsActive=true, CreatedAt=now,...) nên ở trong service
            await _chatRoomService.CreateAsync(chatRoom);

            // Trả về DTO
            var response = _mapper.Map<ChatRoomResponse>(chatRoom);
            return CreatedAtAction(nameof(Get), new { id = response.ChatRoomId }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ChatRoomUpdateRequest request)
        {
            // Lấy entity_gốc từ DB
            var existingChatRoom = await _chatRoomService.GetByIdAsync(id);
            if (existingChatRoom == null) return NotFound();

            // Map các trường được phép thay đổi từ DTO vào entity_gốc
            _mapper.Map(request, existingChatRoom);

            // Cập nhật entity đã được_map
            await _chatRoomService.UpdateAsync(existingChatRoom);
            return NoContent(); // Hoặc Ok(_mapper.Map<ChatRoomResponse>(existingChatRoom))
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var chatRoom = await _chatRoomService.GetByIdAsync(id);
            if (chatRoom == null) return NotFound();

            await _chatRoomService.DeleteAsync(id);
            return NoContent();
        }
    }
}