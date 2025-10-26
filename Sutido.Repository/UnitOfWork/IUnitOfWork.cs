using Sutido.Model.Entites;
using Sutido.Repository.Generic;
using Sutido.Repository.Interfaces;

namespace Sutido.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepo Users { get; }
        public ITutorProfileRepo TutorProfiles { get; }
        public ICertificationRepo Certifications { get; }
        Task<int> SaveAsync();
    }
}
