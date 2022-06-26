using AutoMapper;
using OngProject.Core.Models.DTOs.Users;
using OngProject.Entities;

namespace Test.Mapper
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}
