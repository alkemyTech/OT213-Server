using AutoMapper;
using OngProject.Core.Models.DTOs.Members;
using OngProject.Core.Models.DTOs.Organizations;
using OngProject.Core.Models.DTOs.Users.Auth;
using OngProject.Entities;

namespace OngProject.Core.Helper
{
    public class ApiMapper : Profile
    {
        public ApiMapper()
        {     
            #region Mapper Members      
            // GetRequest -> Member
            CreateMap<Member, MemberGetModelDTO>().ReverseMap();
            // CreateRequest -> Member
            CreateMap<Member, MemberCreateModelDTO>().ReverseMap();
            // UpdateRequest -> Member
            CreateMap<Member, MemberUpdateModelDTO>().ReverseMap();
            #endregion
            
            #region Mapper Users      
            // Register -> Users
            CreateMap<User, UserRegisterModelDTO>().ReverseMap();
            #endregion

            #region Organization Mapper
            CreateMap<Organization, OrganizationGetDTO>();
            CreateMap<OrganizationCreateDTO, Organization>();
            CreateMap<OrganizationUpdateDTO, Organization>();
            #endregion
        }
    }

}

