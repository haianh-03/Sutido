using Sutido.Model.Entites;

namespace Sutido.Repository.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetAllAsync();
        Task<Message?> GetByIdAsync(long id);
        Task AddAsync(Message message);
        void Update(Message message);
        void Delete(Message message);

        // ✅ Thêm mới:
        Task<IEnumerable<Message>> GetMessagesByChatRoomIdAsync(long chatRoomId);
        Task<IEnumerable<Message>> GetMessagesByChatRoomAndSenderAsync(long chatRoomId, long senderId);
    }
}
