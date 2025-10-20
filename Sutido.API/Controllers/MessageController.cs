using Microsoft.AspNetCore.Mvc;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;

namespace SutidoWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // ==========================
        // 📘 1️⃣ Lấy tất cả tin nhắn
        // ==========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _messageService.GetAllAsync();
            return Ok(messages);
        }

        // ==========================
        // 📘 2️⃣ Lấy tin nhắn theo ID
        // ==========================
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null)
                return NotFound($"Không tìm thấy tin nhắn có ID = {id}");
            return Ok(message);
        }

        // ==========================
        // ✉️ 3️⃣ Thêm tin nhắn mới
        // ==========================
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Message message)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _messageService.AddAsync(message);
            return CreatedAtAction(nameof(GetById), new { id = message.MessageId }, message);
        }

        // ==========================
        // 🛠️ 4️⃣ Cập nhật tin nhắn
        // ==========================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Message message)
        {
            if (id != message.MessageId)
                return BadRequest("ID không khớp.");

            var existing = await _messageService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Không tìm thấy tin nhắn có ID = {id}");

            _messageService.Update(message);
            return NoContent();
        }

        // ==========================
        // ❌ 5️⃣ Xóa tin nhắn
        // ==========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var existing = await _messageService.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Không tìm thấy tin nhắn có ID = {id}");

            _messageService.Delete(existing);
            return NoContent();
        }

        // ==========================
        // 💬 6️⃣ Lấy tin nhắn theo ChatRoom
        // ==========================
        [HttpGet("chatroom/{chatRoomId}")]
        public async Task<IActionResult> GetMessagesByChatRoomId(long chatRoomId)
        {
            var messages = await _messageService.GetMessagesByChatRoomIdAsync(chatRoomId);
            return Ok(messages);
        }

        // ==========================
        // 💬 7️⃣ Lấy tin nhắn theo ChatRoom + Sender
        // ==========================
        [HttpGet("chatroom/{chatRoomId}/sender/{senderId}")]
        public async Task<IActionResult> GetMessagesByChatRoomAndSender(long chatRoomId, long senderId)
        {
            var messages = await _messageService.GetMessagesByChatRoomAndSenderAsync(chatRoomId, senderId);
            return Ok(messages);
        }
    }
}
