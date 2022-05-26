using System;
using System.Threading.Tasks;
using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class RoleRepository : GenericRepository<Roles>, IRoleRepository
    {
        public RoleRepository(OngProjectDbContext context) : base(context)
        {
        }
    }

}

