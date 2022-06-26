using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class SlidesBusiness : GenericBusiness<Slide>, ISlidesBusiness
    {
        public SlidesBusiness(IUnitOfWork uow, ISlidesRepository slidesRepository) : base(slidesRepository, uow)
        {
        }
    }
}
