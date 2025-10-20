using Sutido.Model.Entites;

namespace Sutido.Repository.Interfaces
{
    public interface IChatRoomRepository
    {
        Task<IEnumerable<ChatRoom>> GetAllAsync();
        Task<ChatRoom?> GetByIdAsync(long id);
        Task AddAsync(ChatRoom chatRoom);
        void Update(ChatRoom chatRoom);
        void Delete(ChatRoom chatRoom);
    }
}
