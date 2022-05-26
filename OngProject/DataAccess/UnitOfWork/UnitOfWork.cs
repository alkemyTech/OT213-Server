using System.Threading.Tasks;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;

namespace OngProject.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OngProjectDbContext _context;        
        public IMemberRepository Members {get; private set;}
        public IRoleRepository Roles { get; private set; }

        public UnitOfWork(OngProjectDbContext context)
        {
            this._context = context;
            Members = new MemberRepository(_context);  
            Roles = new RoleRepository(_context);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }    

    }

}

