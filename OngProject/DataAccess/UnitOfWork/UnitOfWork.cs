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
        public IMemberRepository Members { get; private set; }
        public IRoleRepository Role { get; private set; }
        public ITestimonialRepository Testimonials { get; private set; }
        public IAuthRepository Authentications { get; private set; }
        public IOrganizationRepository Organizations { get; private set; }
        public IActivitiesRepository Activities { get; private set; }
        public ICategoryRepository Categories { get; private set; }

        public INewsRepository News { get; private set; }

        public IUsersRepository Users { get; private set; }
        public ISlidesRepository Slides { get; private set; }


        public UnitOfWork(OngProjectDbContext context)
        {
            this._context = context;
            Members = new MemberRepository(_context);
            Role = new RoleRepository(_context);
            News = new NewsRepository(_context);
            Users = new UsersRepository(_context);
            Testimonials = new TestimonialRepository(_context);
            Authentications = new AuthRepository(_context);
            Organizations = new OrganizationRepository(_context);
            Activities = new ActivitiesRepository(_context);
            Categories = new CategoryRepository(_context);
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