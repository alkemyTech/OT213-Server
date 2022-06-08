using AutoMapper;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.DTOs.Activities;
using OngProject.Core.Models.DTOs.Members;
using OngProject.Core.Models.DTOs.Organizations;
using OngProject.Core.Models.DTOs.Testimonial;
using OngProject.Core.Models.DTOs.Users.Auth;
using OngProject.Entities;

namespace OngProject.Core.Mapper
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
            
            #region Mapper Users & Rol      
            // Register -> Users
            CreateMap<User, UserRegisterModelDTO>().ReverseMap();
            CreateMap<Role, RoleModelDto>().ReverseMap();
            #endregion

            #region Organization Mapper
            CreateMap<Organization, OrganizationGetDTO>().ReverseMap();
            CreateMap<Organization, OrganizationCreateDTO>().ReverseMap();
            CreateMap<Organization, OrganizationUpdateDTO>().ReverseMap();
            #endregion

            #region Activities Mapper
            CreateMap<Activities, ActivitiesGetDTO>();
            CreateMap<ActivityCreateDTO, Activities>();
            CreateMap<ActivityUpdateDTO, Activities>();
            #endregion

            #region Testimonial Mapper
            CreateMap<Testimonial, TestimonialGetDTO>().ReverseMap();
            CreateMap<Testimonial, TestimonialCreateDTO>().ReverseMap();
            CreateMap<Testimonial, TestimonialUpdateDTO>().ReverseMap();
            #endregion
        }
    }

}

