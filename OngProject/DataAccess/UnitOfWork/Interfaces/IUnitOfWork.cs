using System;
using System.Threading.Tasks;
using OngProject.Repositories.Auth.Interfaces;
using OngProject.Repositories.Interfaces;

namespace OngProject.DataAccess.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Write here all the entities Non-Generics
        IMemberRepository Members {get;}
        INewsRepository News { get; }
        IUsersRepository Users { get; }
        //IMemberBusiness Members2 {get;} // Fail at implementation the business layer        
        IRoleRepository Role { get; }
        IAuthRepository Authentications {get;}
        IOrganizationRepository Organizations { get; }
        //ISlideRepository Slides { get; }
        ICategoryRepository Categories { get; }
        //INewRepository News { get; }
        IActivitiesRepository Activities { get; }
        ITestimonialRepository Testimonials { get; }
        ISlidesRepository Slides { get; }
        Task SaveAsync();

    }

}

