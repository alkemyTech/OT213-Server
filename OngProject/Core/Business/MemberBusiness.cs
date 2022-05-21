using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class MemberBusiness : GenericBusiness<Member>, IMemberBusiness
    {
        private IMemberRepository _memberRepository;
        public MemberBusiness(IMemberRepository memberRepository) : base(memberRepository)
        {
            this._memberRepository = memberRepository;
        }
    }
}
