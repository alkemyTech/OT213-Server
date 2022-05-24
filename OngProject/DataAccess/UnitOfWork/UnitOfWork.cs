using System.Threading.Tasks;
using OngProject.Core.Business;
using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;

namespace OngProject.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OngProjectDbContext _context;        
        public IMemberRepository Members {get; private set;}
        //public IMemberBusiness Members2 {get; private set;}


        public UnitOfWork(OngProjectDbContext context)
        {
            this._context = context;
            Members = new MemberRepository(_context);

            /*
                Error here because MemberBusiness doesn't implement a context class, instead MemberRepository
            */
            
            //Members2 = new MemberBusiness(_context);  
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

