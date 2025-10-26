using Sutido.Model.Data;
using Sutido.Model.Entites;
using Sutido.Repository.Generic;
using Sutido.Repository.Interfaces;

namespace Sutido.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SutidoProjectContext _context;

        public IUserRepo Users { get; }
        public ITutorProfileRepo TutorProfiles { get; }
        public ICertificationRepo Certifications { get; }

        public UnitOfWork(SutidoProjectContext context,
            IUserRepo iUserRepo,
            ITutorProfileRepo iTutorProfileRepo,
            ICertificationRepo iCertificationRepo)
        {
            _context = context;
            Users = iUserRepo;
            TutorProfiles = iTutorProfileRepo;
            Certifications = iCertificationRepo;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
