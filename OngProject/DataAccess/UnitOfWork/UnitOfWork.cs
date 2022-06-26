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
        public IMembersRepository Members { get; private set; }
        public IRoleRepository Role { get; private set; }
        public ITestimonialsRepository Testimonials { get; private set; }
        public IAuthRepository Authentications { get; private set; }
        public IOrganizationsRepository Organizations { get; private set; }
        public IActivitiesRepository Activities { get; private set; }
        public ICategoriesRepository Categories { get; private set; }

        public INewsRepository News { get; private set; }

        public IUsersRepository Users { get; private set; }
        public ISlidesRepository Slides { get; private set; }
        public ICommentsRepository Comments { get; private set; }


        public UnitOfWork(OngProjectDbContext context)
        {
            this._context = context;
            Members = new MembersRepository(_context);
            Role = new RolesRepository(_context);
            News = new NewsRepository(_context);
            Users = new UsersRepository(_context);
            Testimonials = new TestimonialsRepository(_context);
            Authentications = new AuthRepository(_context);
            Organizations = new OrganizationsRepository(_context);
            Activities = new ActivitiesRepository(_context);
            Categories = new CategoriesRepository(_context);
            Comments = new CommentRepository(_context);
            Slides = new SlidesRepository(_context);
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