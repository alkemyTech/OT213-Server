using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class CategoryBusiness : GenericBusiness<Category>, ICategoryBusiness
    {
        public CategoryBusiness(IUnitOfWork uow, ICategoryRepository memberRepository) : base(memberRepository, uow)
        {            
        }
    }
    
}

