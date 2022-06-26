using AutoMapper;
using OngProject.Core.Models.DTOs.Users.Auth;
using OngProject.Entities;

namespace Test.Mapper
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<UserAuthDTO, User>();
            CreateMap<User, UserAuthDTO>();
        }
    }
}
