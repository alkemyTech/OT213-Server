using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        public IEnumerable<Member> FindMemberAsync(Expression<Func<Member, bool>> predicate)
        {
            return  _uow.Members.FindMemberAsync(predicate);
        }

        public Task<Member> GetMemberByIdAsync(int id)
        {
            return _uow.Members.GetMemberByIdAsync(id);
        }

        public Task<Member> InsertMemberAsync(Member entity)
        {
            return _uow.Members.InsertMemberAsync(entity);
        }

        // Soft Delete
        public async Task<bool> SoftDelete(Member entity, int? id)
        {
            return await _uow.Members.SoftDelete(entity, id);
        }

        public async Task<Member> UpdateMemberAsync(Member entity)
        {
            return await _uow.Members.UpdateMemberAsync(entity);
        }
    }
}
