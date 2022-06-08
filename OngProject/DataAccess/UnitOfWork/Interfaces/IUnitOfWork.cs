using System;
using System.Threading.Tasks;
using OngProject.Repositories.Auth.Interfaces;
using OngProject.Repositories.Interfaces;

namespace OngProject.DataAccess.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Write here all the entities (Interfaces Non-Generics)
        IMemberRepository Members {get;}
        INewsRepository News { get; }
        IUsersRepository Users { get; }
        IRoleRepository Role { get; }
        IAuthRepository Authentications {get;}
        IOrganizationRepository Organizations { get; }
        ICategoryRepository Categories { get; }
        IActivitiesRepository Activities { get; }
        ITestimonialRepository Testimonials { get; }
        ISlidesRepository Slides { get; }
        ICommentRepository Comments { get; }
        Task SaveAsync();

    }

}

