using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class RoleBusiness : GenericBusiness<Roles>, IRoleBusiness
    {
        public RoleBusiness(IUnitOfWork uow, IRoleRepository roleRepository) : base(roleRepository)
        {
        }
    }
}
