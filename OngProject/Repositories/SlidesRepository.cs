using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class SlidesRepository : GenericRepository<Slide>, ISlidesRepository
    {
        public SlidesRepository(OngProjectDbContext context) : base(context)
        {
        }
    }

}

