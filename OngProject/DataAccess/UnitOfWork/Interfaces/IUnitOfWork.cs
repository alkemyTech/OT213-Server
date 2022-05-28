using System;
using System.Threading.Tasks;
using OngProject.Repositories.Interfaces;

namespace OngProject.DataAccess.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        // Write here all the entities Non-Generics
        IMemberRepository Members {get;}
        IRoleRepository Roles { get; }

        // Methods
        Task SaveAsync();
    }

}

