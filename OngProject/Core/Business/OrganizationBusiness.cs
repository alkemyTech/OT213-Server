using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class OrganizationBusiness : GenericBusiness<Organization>, IOrganizationBusiness
    {
        public OrganizationBusiness(IUnitOfWork uow, IOrganizationRepository organizationRepository) : base(organizationRepository, uow)
        {
        }
    }
}
