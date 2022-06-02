using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class MemberBusiness : GenericBusiness<Member>, IMemberBusiness
    {
        public MemberBusiness(IUnitOfWork uow, IMemberRepository memberRepository) : base(memberRepository, uow)
        {
        }

    }
}
