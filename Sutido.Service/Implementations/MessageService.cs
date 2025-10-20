using Sutido.Model.Entites;
using Sutido.Repository.Interfaces;
using Sutido.Service.Interfaces;

namespace Sutido.Service.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;

        public MessageService(IMessageRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Message>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<Message?> GetByIdAsync(long id) =>
            await _repository.GetByIdAsync(id);

        public async Task AddAsync(Message message) =>
            await _repository.AddAsync(message);

        public void Update(Message message) =>
            _repository.Update(message);

        public void Delete(Message message) =>
            _repository.Delete(message);

        // ✅ Thêm mới:
        public async Task<IEnumerable<Message>> GetMessagesByChatRoomIdAsync(long chatRoomId) =>
            await _repository.GetMessagesByChatRoomIdAsync(chatRoomId);

        public async Task<IEnumerable<Message>> GetMessagesByChatRoomAndSenderAsync(long chatRoomId, long senderId) =>
            await _repository.GetMessagesByChatRoomAndSenderAsync(chatRoomId, senderId);
    }
}
