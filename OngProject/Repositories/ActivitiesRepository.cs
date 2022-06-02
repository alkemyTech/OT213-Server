using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class ActivitiesRepository : GenericRepository<Activities>, IActivitiesRepository
    {
        public ActivitiesRepository(OngProjectDbContext context) : base(context) { }
    }
}