using OngProject.Core.Models.DTOs.Roles;
using OngProject.Entities;
using System;

namespace OngProject.Core.Mapper
{
    public class EntityMapper
    {
        public Role ToNewEntity(RoleModelDto roleDto)
        {
            return new Role()
            {
                Name = roleDto.Name,
                Description = roleDto.Description
            };
        }

        public Role ToUpdateEntity(RoleModelDto roleDto)
        {
            return new Role()
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                UpdatedAt = DateTime.Now
            };
        }

        public RoleModelDto ToModelDto(Role role)
        {
            return new RoleModelDto()
            {
                Name = role.Name,
            };
        }
    }
}
