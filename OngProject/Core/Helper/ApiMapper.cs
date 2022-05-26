using AutoMapper;
using OngProject.Core.Models.DTOs.Members;
using OngProject.Entities;

namespace OngProject.Core.Helper
{
    public class ApiMapper : Profile
    {
        public ApiMapper()
        {            
            // GetRequest -> Member
            CreateMap<Member, MemberGetModelDTO>().ReverseMap();

            // CreateRequest -> Member
            CreateMap<Member, MemberCreateModelDTO>().ReverseMap();

            // UpdateRequest -> Member
            CreateMap<Member, MemberUpdateModelDTO>().ReverseMap();
        }
    }

}

