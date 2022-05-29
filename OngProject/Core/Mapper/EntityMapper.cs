using OngProject.Core.Models;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Mapper
{
    public class EntityMapper
    {
        public Roles ToNewEntity(RoleModelDto roleDto)
        {
            return new Roles()
            {
                Name = roleDto.Name,
            };
        }

        public Roles ToUpdateEntity(RoleModelDto roleDto)
        {
            return new Roles()
            {
                Id = roleDto.Id,
                Name = roleDto.Name,
                TimeStamp = DateTime.Now
            };
        }

        public RoleModelDto ToModelDto(Roles role)
        {
            return new RoleModelDto()
            {
                Name = role.Name,
            };
        }
    }
}
