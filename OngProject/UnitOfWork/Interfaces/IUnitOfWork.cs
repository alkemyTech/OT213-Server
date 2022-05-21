using System;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Write all the entities that point to the same DbContext
        IMemberRepository Members {get;}
        // Categories... 
        // News...
        // etc.
        
        Task CompleteAsync();
    }

}

