using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sutido.Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        Task<int> SaveAsync();
    }
}
