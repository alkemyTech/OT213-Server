using System;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Repositories.Interfaces;

namespace OngProject.DataAccess.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Write all the entities that point to the same DbContext
        IMemberRepository Members {get;}
        IRoleRepository Roles { get; }
        INewsRepository News { get; }
        IUsersRepository Users { get; }
        //IMemberBusiness Members2 {get;} // Fail at implementation the business layer        

        // Methods
        Task SaveAsync();
        // Task BeginTransaction();
        // Task<bool> Commit();
        // Task Rollback();
    }

}

