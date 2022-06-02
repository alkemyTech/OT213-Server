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
<<<<<<< HEAD
        IRoleRepository Roles { get; }
        INewsRepository News { get; }
        IUsersRepository Users { get; }
        //IMemberBusiness Members2 {get;} // Fail at implementation the business layer        

        // Methods
=======
        IRoleRepository Roles { get; }
        IAuthRepository Authentications {get;}
        IOrganizationRepository Organizations { get; }
        //ISlideRepository Slides { get; }
        ICategoryRepository Categories { get; }
        //INewRepository News { get; }
        IActivitiesRepository Activities { get; }
        ITestimonialRepository Testimonials { get; }
        
        // Methods
>>>>>>> e38780518ca654d41d3d3b361602e9fd3f15b52b
        Task SaveAsync();

    }

}

