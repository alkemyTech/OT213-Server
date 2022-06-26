using OngProject.DataAccess;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Repositories
{
    public class TestimonialsRepository : GenericRepository<Testimonial>, ITestimonialsRepository
    {
        public TestimonialsRepository(OngProjectDbContext context) : base(context)
        {
        }
    }
}
