using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class ActivitiesBusiness : GenericBusiness<Activity>, IActivitiesBusiness
    {
        public ActivitiesBusiness(IUnitOfWork uow, IActivitiesRepository activitiesRepository) : base(activitiesRepository, uow)
        {

        }
    }
}
