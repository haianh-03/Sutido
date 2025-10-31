using Sutido.Model.Data;
using Sutido.Repository.Interfaces;

namespace Sutido.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SutidoProjectContext _context;

        public IBookingRepository Bookings { get; }
        public IChatRoomRepository ChatRooms { get; }
        public IMessageRepository Messages { get; }
        public IReviewRepository Reviews { get; }
        public ITrackingRepository Trackings { get; }
        public IUserRepo Users { get; }
        public ITutorProfileRepo TutorProfiles { get; }
        public ICertificationRepo Certifications { get; }
        public IPostRepository Posts { get; }

        public UnitOfWork(SutidoProjectContext context,
            IUserRepo iUserRepo,
            ITutorProfileRepo iTutorProfileRepo,
            ICertificationRepo iCertificationRepo,
            IBookingRepository iBookingRepo,
            IChatRoomRepository iChatRoomRepo,
            IMessageRepository iMessageRepo,
            IReviewRepository iReviewRepo,
            ITrackingRepository iTrackingRepo,
            IPostRepository posts)
        {
            _context = context;

            Users = iUserRepo;
            TutorProfiles = iTutorProfileRepo;
            Certifications = iCertificationRepo;
            Bookings = iBookingRepo;
            ChatRooms = iChatRoomRepo;
            Messages = iMessageRepo;
            Reviews = iReviewRepo;
            Trackings = iTrackingRepo;
            Posts = posts;
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
