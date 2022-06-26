using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class OrganizationsRepository : GenericRepository<Organization>, IOrganizationsRepository
    {
        public OrganizationsRepository(OngProjectDbContext context) : base(context) { }
    }
}
