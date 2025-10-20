using Sutido.Model.Entites;

namespace Sutido.Service.Interfaces
{
    public interface IChatRoomService
    {
        Task<IEnumerable<ChatRoom>> GetAllAsync();
        Task<ChatRoom?> GetByIdAsync(long id);
        Task CreateAsync(ChatRoom chatRoom);
        Task UpdateAsync(ChatRoom chatRoom);
        Task DeleteAsync(long id);
    }
}
