using AutoMapper;
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
            
            #region Mapper Users      
            // Register -> Users
            CreateMap<User, UserRegisterModelDTO>().ReverseMap();
            #endregion

            #region Organization Mapper
            CreateMap<Organization, OrganizationGetDTO>().ReverseMap();
            CreateMap<Organization, OrganizationCreateDTO>().ReverseMap();
            CreateMap<Organization, OrganizationUpdateDTO>().ReverseMap();
            #endregion

            #region Activities Mapper
            CreateMap<Activities, ActivitiesGetDTO>().ReverseMap();
            CreateMap<Activities, ActivityCreateDTO>().ReverseMap();
            CreateMap<Activities, ActivityUpdateDTO>().ReverseMap();
            #endregion

            #region Testimonial Mapper
            CreateMap<Testimonial, TestimonialGetDTO>().ReverseMap();
            CreateMap<Activities, TestimonialCreateDTO>().ReverseMap();
            CreateMap<Activities, TestimonialUpdateDTO>().ReverseMap();
            #endregion
        }
    }

}

