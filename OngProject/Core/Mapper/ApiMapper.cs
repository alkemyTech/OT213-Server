using AutoMapper;
using OngProject.Core.Models.DTOs.Activities;
using OngProject.Core.Models.DTOs.Categories;
using OngProject.Core.Models.DTOs.Comments;
using OngProject.Core.Models.DTOs.Members;
using OngProject.Core.Models.DTOs.News;
using OngProject.Core.Models.DTOs.Organizations;
using OngProject.Core.Models.DTOs.Roles;
using OngProject.Core.Models.DTOs.Slides;
using OngProject.Core.Models.DTOs.Testimonials;
using OngProject.Core.Models.DTOs.Users;
using OngProject.Core.Models.DTOs.Users.Auth;
using OngProject.Entities;

namespace OngProject.Core.Mapper
{
    public class ApiMapper : Profile
    {
        public ApiMapper()
        {     
            #region Mapper Members      
            CreateMap<Member, MemberResponse>().ReverseMap();
            CreateMap<Member, MemberRequest>().ReverseMap();
            #endregion
            
            #region Mapper Users     
            CreateMap<User, UserAuthDTO>().ReverseMap();
            CreateMap<User, UserGetDTO>();
            CreateMap<User, UserResponse>().ReverseMap();
            #endregion

            #region Mapper Rol 
            CreateMap<Role, RoleResponse>().ReverseMap();
            CreateMap<Role, RoleRequest>().ReverseMap();
            #endregion

            #region Organization Mapper
            CreateMap<Organization, OrganizationResponse>().ReverseMap();
            CreateMap<Organization, OrganizationRequest>().ReverseMap();
            #endregion

            #region Activities Mapper
            CreateMap<Activity, ActivityResponse>();
            CreateMap<ActivityRequest, Activity>();
            #endregion

            #region Testimonial Mapper
            CreateMap<Testimonial, TestimonialResponse>().ReverseMap();
            CreateMap<Testimonial, TestimonialRequest>().ReverseMap();
            #endregion

            #region Categories Mapper
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Category, CategoryRequest>().ReverseMap();
            #endregion

            #region Slides Mapper
            CreateMap<Slide, SlideRequest>().ReverseMap();
            CreateMap<Slide, SlideResponse>().ReverseMap();
            #endregion

            #region Comments Mapper
            CreateMap<Comment, CommentResponse>().ReverseMap();
            CreateMap<Comment, CommentRequest>().ReverseMap();
            #endregion

            #region New Mapper
            CreateMap<News, NewsRequest>().ReverseMap();
            CreateMap<News, NewsResponse>().ReverseMap();
            #endregion

        }
    }

}

