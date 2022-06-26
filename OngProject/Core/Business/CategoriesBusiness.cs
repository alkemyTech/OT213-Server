using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class CategoriesBusiness : GenericBusiness<Category>, ICategoriesBusiness
    {
        public CategoriesBusiness(IUnitOfWork uow, ICategoriesRepository categoryRepository) : base(categoryRepository, uow)
        {            
        }
    }
    
}

