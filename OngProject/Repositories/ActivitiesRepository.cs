using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class ActivitiesRepository : GenericRepository<Activity>, IActivitiesRepository
    {
        public ActivitiesRepository(OngProjectDbContext context) : base(context) {    }
    }
}
