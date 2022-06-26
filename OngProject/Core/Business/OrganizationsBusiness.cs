using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class OrganizationsBusiness : GenericBusiness<Organization>, IOrganizationsBusiness
    {
        public OrganizationsBusiness(IUnitOfWork uow, IOrganizationsRepository organizationRepository) : base(organizationRepository, uow)
        {
        }
    }
}
