using System;
using System.Threading.Tasks;
using OngProject.Repositories.Auth.Interfaces;
using OngProject.Repositories.Interfaces;

namespace OngProject.DataAccess.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Write here all the entities (Interfaces Non-Generics)
        IMembersRepository Members {get;}
        INewsRepository News { get; }
        IUsersRepository Users { get; }
        IRoleRepository Role { get; }
        IAuthRepository Authentications {get;}
        IOrganizationsRepository Organizations { get; }
        ICategoriesRepository Categories { get; }
        IActivitiesRepository Activities { get; }
        ITestimonialsRepository Testimonials { get; }
        ISlidesRepository Slides { get; }
        ICommentsRepository Comments { get; }
        Task SaveAsync();

    }

}

