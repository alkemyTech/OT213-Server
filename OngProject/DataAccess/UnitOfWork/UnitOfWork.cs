using System.Threading.Tasks;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Repositories;
using OngProject.Repositories.Auth;
using OngProject.Repositories.Auth.Interfaces;
using OngProject.Repositories.Interfaces;

namespace OngProject.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OngProjectDbContext _context;        
        public IMemberRepository Members {get; private set;}
        public IRoleRepository Roles { get; private set; }

        public ITestimonialRepository Testimonials { get; private set; }
        public IAuthRepository Authentications { get; private set; }
        public IOrganizationRepository Organizations { get; private set; }
        public IActivitiesRepository Activities { get; private set; }
        public UnitOfWork(OngProjectDbContext context)
        {
            this._context = context;
            Members = new MemberRepository(_context);
            Testimonials = new TestimonialRepository(_context);
            Authentications = new AuthRepository(_context);
            Organizations = new OrganizationRepository(_context);
            Activities = new ActivitiesRepository(_context);
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

