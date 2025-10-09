using Microsoft.EntityFrameworkCore;
using Sutido.Model;
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
        public UnitOfWork(SutidoProjectContext context)
        {
            _context = context;
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
