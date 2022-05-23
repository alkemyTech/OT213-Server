using System;
using System.Threading.Tasks;
using OngProject.Repositories.Interfaces;

namespace OngProject.DataAccess.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Write all the entities that point to the same DbContext
        IMemberRepository Members {get;}
        // Categories... 
        // News...
        // etc.        
        
        // Methods
        Task SaveAsync();
        // Task BeginTransaction();
        // Task<bool> Commit();
        // Task Rollback();
    }

}

