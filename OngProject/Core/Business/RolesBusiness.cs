using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class RolesBusiness : GenericBusiness<Role>, IRolesBusiness
    {
        public RolesBusiness(IUnitOfWork uow, IRoleRepository roleRepository) : base(roleRepository, uow)
        {
        }
    }
}
