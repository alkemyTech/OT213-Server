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
        //IMemberBusiness Members2 {get;} // Fail at implementation the business layer        
        IOrganizationRepository Organization { get; }
        // Methods
        Task SaveAsync();
        // Task BeginTransaction();
        // Task<bool> Commit();
        // Task Rollback();
    }

}

