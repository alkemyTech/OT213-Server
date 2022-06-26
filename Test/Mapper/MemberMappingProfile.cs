using AutoMapper;
using OngProject.Core.Models.DTOs.Members;
using OngProject.Entities;

namespace Test.Mapper
{
    public class MemberMappingProfile : Profile
    {
        public MemberMappingProfile()
        {
            CreateMap<MemberRequest, Member>();
            CreateMap<Member, MemberResponse>();
        }
    }
}
