using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class TestimonialBusiness : GenericBusiness<Testimonial>, ITestimonialBusiness
    {
        public TestimonialBusiness(IUnitOfWork uow, ITestimonialRepository testimonialRepository) : base(testimonialRepository, uow)
        {
        }
    }
}
