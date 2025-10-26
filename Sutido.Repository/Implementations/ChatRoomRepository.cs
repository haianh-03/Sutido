using Microsoft.EntityFrameworkCore;
using Sutido.Model;
using Sutido.Model.Data;
using Sutido.Model.Entites;
using Sutido.Repository.Interfaces;

namespace Sutido.Repository.Implementations
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly SutidoProjectContext _context;

        public ChatRoomRepository(SutidoProjectContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChatRoom>> GetAllAsync() =>
            await _context.ChatRooms.ToListAsync();

        public async Task<ChatRoom?> GetByIdAsync(long id) =>
            await _context.ChatRooms.FindAsync(id);

        public async Task AddAsync(ChatRoom chatRoom) =>
            await _context.ChatRooms.AddAsync(chatRoom);

        public void Update(ChatRoom chatRoom) =>
            _context.ChatRooms.Update(chatRoom);

        public void Delete(ChatRoom chatRoom) =>
            _context.ChatRooms.Remove(chatRoom);
    }
}
