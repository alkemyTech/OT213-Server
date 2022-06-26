using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class RolesRepository : GenericRepository<Role>, IRoleRepository
    {
        public RolesRepository(OngProjectDbContext context) : base(context)
        {
        }
    }

}

