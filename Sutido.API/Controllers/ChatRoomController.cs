using Microsoft.AspNetCore.Mvc;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;

namespace Sutido.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;

        public ChatRoomController(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _chatRoomService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var chatRoom = await _chatRoomService.GetByIdAsync(id);
            return chatRoom == null ? NotFound() : Ok(chatRoom);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ChatRoom chatRoom)
        {
            await _chatRoomService.CreateAsync(chatRoom);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, ChatRoom chatRoom)
        {
            if (id != chatRoom.ChatRoomId) return BadRequest();
            await _chatRoomService.UpdateAsync(chatRoom);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _chatRoomService.DeleteAsync(id);
            return Ok();
        }
    }
}
