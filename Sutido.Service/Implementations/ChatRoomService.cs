using Sutido.Model.Entites;
using Sutido.Repository.UnitOfWork;
using Sutido.Service.Interfaces;

namespace Sutido.Service.Implementations
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChatRoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ChatRoom>> GetAllAsync() =>
            await _unitOfWork.ChatRooms.GetAllAsync();

        public async Task<ChatRoom?> GetByIdAsync(long id) =>
            await _unitOfWork.ChatRooms.GetByIdAsync(id);

        public async Task CreateAsync(ChatRoom chatRoom)
        {
            await _unitOfWork.ChatRooms.AddAsync(chatRoom);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(ChatRoom chatRoom)
        {
            _unitOfWork.ChatRooms.Update(chatRoom);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var room = await _unitOfWork.ChatRooms.GetByIdAsync(id);
            if (room != null)
            {
                _unitOfWork.ChatRooms.Delete(room);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
