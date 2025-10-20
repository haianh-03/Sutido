using Sutido.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sutido.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBookingRepository Bookings { get; }
        IChatRoomRepository ChatRooms { get; }
        IMessageRepository Messages { get; }
        IReviewRepository Reviews { get; }
        ITrackingRepository Trackings { get; }

        Task<int> SaveAsync();
    }
}
