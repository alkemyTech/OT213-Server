using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class TestimonialsBusiness : GenericBusiness<Testimonial>, ITestimonialsBusiness
    {
        public TestimonialsBusiness(IUnitOfWork uow, ITestimonialsRepository testimonialRepository) : base(testimonialRepository, uow)
        {
        }
    }
}
