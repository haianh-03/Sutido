using Microsoft.EntityFrameworkCore;
using Sutido.Model;
using Sutido.Repository.Implementations;
using Sutido.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public UnitOfWork(SutidoProjectContext context)
        {
            _context = context;
            Bookings = new BookingRepository(_context);
            ChatRooms = new ChatRoomRepository(_context);
            Messages = new MessageRepository(_context);
            Reviews = new ReviewRepository(_context);
            Trackings = new TrackingRepository(_context);
        }

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
