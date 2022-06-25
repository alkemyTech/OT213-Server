using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs.Roles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleBusiness _rolBusiness;
        private readonly IMapper _mapper;
        public RoleController(IRoleBusiness rolBusiness, IMapper mapper)
        {
            this._rolBusiness = rolBusiness;
            this._mapper = mapper;
        }

        [HttpGet]    
        [Route("/Role/all")]
        public  IActionResult GetAllRoles()
        {
            var role = _rolBusiness.Find(c => c.IsDeleted == false);
            return Ok(_mapper.Map<IEnumerable<RoleGetDTO>>(role));
        }

        [HttpGet]
        [Route("/Role/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _rolBusiness.GetById(id);
            return Ok(_mapper.Map<RoleGetDTO>(role));
        }

        [HttpPost]
        [Route("/Role/Create")]
        public async Task<IActionResult> CreateRole(RoleCreateDto role)
        {
            var result = await _rolBusiness.Insert(new EntityMapper().ToNewEntity(role));
            if (result is not null) 
                return Ok($"{role.Name} role created");
            else 
                return BadRequest("Error in Creating the role");
        }

        [HttpPut]
        [Route("/Role/Update")]
        public async Task<IActionResult> UpdateRole(int id, RoleUpdateDTO dto)
        {
            if (id != dto.Id)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    Status = "Error",
                    Message = "Id number doesn't match!"
                });
            }

            var rol = await _rolBusiness.GetById(id);
            _mapper.Map(dto, rol);
            await _rolBusiness.Update(rol);            
   
            return Ok(new 
            {
                Status = "Success",
                Message = $"{rol.Name} rol updated successfully!"
            }); 
        }

        [HttpDelete]       
        [Route("Role/Delete/{id}")]
        public async Task<IActionResult> DeleteRole(int? id)
        {
            var rol = await _rolBusiness.GetById(id.Value);       
            await _rolBusiness.SoftDelete(rol);
            await _rolBusiness.Update(rol);
            return Ok(new 
            {
                Status = "Success",
                Message = $"{rol.Name} rol deleted successfully!"
            }); 
        }

    }
}