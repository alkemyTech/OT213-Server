using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class NewsBusiness : GenericBusiness<New>, INewsBusiness
    {
        public NewsBusiness(IUnitOfWork uow, INewsRepository newsRepository) : base(newsRepository, uow)
        {
        }
    }
}
