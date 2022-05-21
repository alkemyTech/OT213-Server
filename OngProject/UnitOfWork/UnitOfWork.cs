using System.Threading.Tasks;
using OngProject.DataAccess;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;

namespace OngProject.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OngProjectDbContext _context;
        public IMemberRepository Members {get; private set;}

        public UnitOfWork(OngProjectDbContext context)
        {
            this._context = context;
            Members = new MemberRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}

