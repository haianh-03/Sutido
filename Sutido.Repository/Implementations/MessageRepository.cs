using Microsoft.EntityFrameworkCore;
using Sutido.Model;
using Sutido.Model.Entites;
using Sutido.Repository.Interfaces;

namespace Sutido.Repository.Implementations
{
    public class MessageRepository : IMessageRepository
    {
        private readonly SutidoProjectContext _context;

        public MessageRepository(SutidoProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Message>> GetAllAsync() =>
            await _context.Messages.ToListAsync();

        public async Task<Message?> GetByIdAsync(long id) =>
            await _context.Messages.FindAsync(id);

        public async Task AddAsync(Message message) =>
            await _context.Messages.AddAsync(message);

        public void Update(Message message) =>
            _context.Messages.Update(message);

        public void Delete(Message message) =>
            _context.Messages.Remove(message);


        // ✅ Thêm mới: Lấy danh sách tin nhắn theo ChatRoomId
        public async Task<IEnumerable<Message>> GetMessagesByChatRoomIdAsync(long chatRoomId)
        {
            return await _context.Messages
                .Where(m => m.ChatRoomId == chatRoomId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        // ✅ Thêm mới: Lấy danh sách tin nhắn theo ChatRoomId và SenderId
        public async Task<IEnumerable<Message>> GetMessagesByChatRoomAndSenderAsync(long chatRoomId, long senderId)
        {
            return await _context.Messages
                .Where(m => m.ChatRoomId == chatRoomId && m.SenderId == senderId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
    }
}
