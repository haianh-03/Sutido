using Sutido.Repository.Interfaces;

namespace Sutido.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository Bookings { get; }
        IChatRoomRepository ChatRooms { get; }
        IMessageRepository Messages { get; }
        IReviewRepository Reviews { get; }
        ITrackingRepository Trackings { get; }
        IPostRepository Posts { get; }

        public IUserRepo Users { get; }
        public ITutorProfileRepo TutorProfiles { get; }
        public ICertificationRepo Certifications { get; }
        Task<int> SaveAsync();
    }
}
