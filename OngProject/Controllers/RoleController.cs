using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs.Roles;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleBusiness _rolBusiness;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public RoleController(IUnitOfWork unitOfWork, IRoleBusiness rolBusiness, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this._rolBusiness = rolBusiness;
            this._mapper = mapper;
        }

        [HttpGet(nameof(GetRoles))]
        public async Task<IActionResult> GetRoles() => Ok(await _unitOfWork.Role.GetAll());

        [HttpGet(nameof(GetRoleById))]
        public async Task<IActionResult> GetRoleById([FromQuery] int roleID) => Ok(await _unitOfWork.Role.GetById(roleID));

        [HttpPost(nameof(CreateRole))]
        public async Task<IActionResult> CreateRole(RoleModelDto role)
        {
            var result = await _unitOfWork.Role.Insert(new EntityMapper().ToNewEntity(role));
            await _unitOfWork.SaveAsync();
            if (result is not null) 
                return Ok("Role Created");
            else 
                return BadRequest("Error in Creating the role");
        }

        [HttpPut(nameof(UpdateRole))]
        public async Task<IActionResult> UpdateRole(int id, RoleModelDto dto)
        {
            if (id != dto.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "Id number doesn't match!"
                });
            }

            var rol = await _rolBusiness.GetById(id);
            _mapper.Map(dto, rol);
            var updated = await _rolBusiness.Update(rol);            
   
            return Ok(new 
            {
                Status = "Success",
                Message = $"{rol.Name} rol updated successfully!"
            }); 
        }

        [HttpDelete]       
        [Route("Delete/Rol/{roleID}")]
        public async Task<IActionResult> DeleteRole(int? roleID)
        {
            var rol = await _rolBusiness.GetById(roleID.Value);       
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