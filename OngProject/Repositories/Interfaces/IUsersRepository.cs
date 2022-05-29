using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Repositories.Interfaces
{
    public interface IUsersRepository : IGenericRepository<Users>
    {
        Task<bool> SoftDelete(Users entity, int? id);
    }
}
