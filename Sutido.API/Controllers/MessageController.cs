using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sutido.API.ViewModels.Requests;
using Sutido.API.ViewModels.Responses;
using Sutido.Model.Entites;
using Sutido.Service.Interfaces;
using System.Security.Claims; // Cần để lấy User ID

namespace SutidoWebApplication.Controllers
{
    [Authorize] // 🔒 BẮT BUỘC: Tất cả API chat phải yêu cầu đăng nhập
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        // Thêm các service để lưu file và broadcast SignalR
        // private readonly IFileStorageService _fileService; 
        // private readonly IHubContext<ChatHub> _chatHubContext;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        // ===================================
        // 💬 1. Lấy lịch sử tin nhắn
        // ===================================
        [HttpGet("chatroom/{chatRoomId}")]
        public async Task<IActionResult> GetMessagesByChatRoomId(long chatRoomId)
        {
            // TODO: Kiểm tra xem user hiện tại có phải là thành viên
            // của chatRoomId này không, nếu không thì trả về 403 Forbidden.

            var messages = await _messageService.GetMessagesByChatRoomIdAsync(chatRoomId);

            // Map sang Response DTO
            var response = _mapper.Map<IEnumerable<MessageResponse>>(messages);

            return Ok(response);
        }

        // ===================================
        // ✉️ 2. Gửi tin nhắn TEXT
        // ===================================
        [HttpPost("text")]
        public async Task<IActionResult> CreateTextMessage([FromBody] MessageCreateTextRequest request)
        {
            // Lấy SenderId từ token, KHÔNG BAO GIỜ tin tưởng client
            var senderId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var message = new Message
            {
                ChatRoomId = request.ChatRoomId,
                Content = request.Content,
                SenderId = senderId, // ⬅️ ID an toàn từ token
                MessageType = "text",
                SentAt = DateTimeOffset.UtcNow
            };

            await _messageService.AddAsync(message);

            // TODO: Dùng SignalR để broadcast tin nhắn này tới các client
            // await _chatHubContext.Clients.Group(request.ChatRoomId.ToString())
            //    .SendAsync("ReceiveMessage", _mapper.Map<MessageResponse>(message));

            var response = _mapper.Map<MessageResponse>(message);
            return Ok(response); // Trả về tin nhắn đã tạo
        }

        // ===================================
        // 📎 3. Gửi tin nhắn FILE/ẢNH
        // ===================================
        [HttpPost("file")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateFileMessage([FromForm] MessageCreateFileRequest request)
        {
            var senderId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // TODO: Thêm logic lưu file
            // 1. Gọi _fileService.SaveFileAsync(request.File)
            // 2. Nhận lại fileUrl
            var fileUrl = "https://your-storage.com/path-to-file.jpg"; // Giả sử

            var message = new Message
            {
                ChatRoomId = request.ChatRoomId,
                Content = request.Content, // Caption (nếu có)
                SenderId = senderId,
                MessageType = request.File.ContentType.StartsWith("image") ? "image" : "file",
                FileUrl = fileUrl,
                SentAt = DateTimeOffset.UtcNow
            };

            await _messageService.AddAsync(message);

            // TODO: Dùng SignalR để broadcast

            var response = _mapper.Map<MessageResponse>(message);
            return Ok(response);
        }
    }
}