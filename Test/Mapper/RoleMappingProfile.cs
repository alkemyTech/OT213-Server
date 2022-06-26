using AutoMapper;
using OngProject.Core.Models.DTOs.Roles;
using OngProject.Entities;

namespace Test.Mapper
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<RoleRequest, Role>();
            CreateMap<Role, RoleResponse>();
        }
    }
}
