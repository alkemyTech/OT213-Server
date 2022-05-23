using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class MemberBusiness : GenericBusiness<Member>, IMemberBusiness
    {
        private IUnitOfWork _uow;
        private IMemberRepository _memberRepository;
        public MemberBusiness(IUnitOfWork uow, IMemberRepository memberRepository) : base(memberRepository)
        {
            this._uow = uow;
            this._memberRepository = memberRepository;
        }

        // Soft Delete
        public async Task<bool> SoftDelete(Member entity, int? id)
        {
            return await _uow.Members.SoftDelete(entity, id);
            //return await _memberRepository.SoftDelete(entity, id);
        }

    }
}
