using AutoMapper;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.DTOs.Activities;
using OngProject.Core.Models.DTOs.Categories;
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
            CreateMap<Activities, ActivitiesGetDTO>();
            CreateMap<ActivityCreateDTO, Activities>();
            CreateMap<ActivityUpdateDTO, Activities>();
            #endregion

            #region Testimonial Mapper
            CreateMap<Testimonial, TestimonialGetDTO>().ReverseMap();
            CreateMap<Testimonial, TestimonialCreateDTO>().ReverseMap();
            CreateMap<Testimonial, TestimonialUpdateDTO>().ReverseMap();
            #endregion

            #region Categories Mapper
            CreateMap<Category, CategoryGetDTO>().ReverseMap();
            CreateMap<Category, CategoryCreateDTO>().ReverseMap();
            CreateMap<Category, CategoryDetailsDTO>().ReverseMap();
            CreateMap<Category, CategoryUpdateDTO>().ReverseMap();
            #endregion

            #region Slides Mapper
            CreateMap<Slide, SlideCreateDTO>().ReverseMap();
            CreateMap<Slide, SlideDetailsDTO>().ReverseMap();
            CreateMap<Slide, SlideUpdateDTO>().ReverseMap();
            CreateMap<Slide, SlideGetDTO>().ReverseMap();
            #endregion

            #region Comments Mapper
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Comment, CommentCreateDTO>().ReverseMap();
            CreateMap<Comment, CommentUpdateDTO>().ReverseMap();
            #endregion
        }
    }

}

