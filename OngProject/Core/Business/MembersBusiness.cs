using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class MembersBusiness : GenericBusiness<Member>, IMembersBusiness
    {
        public MembersBusiness(IUnitOfWork uow, IMembersRepository memberRepository) : base(memberRepository, uow)
        {
        }

    }
}
