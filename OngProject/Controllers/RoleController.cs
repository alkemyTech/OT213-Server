using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet(nameof(GetRoles))]
        public async Task<IActionResult> GetRoles() => Ok(await _unitOfWork.Role.GetAll());

        [HttpGet(nameof(GetRoleById))]
        public async Task<IActionResult> GetRoleById([FromQuery] int roleID) => Ok(await _unitOfWork.Role.GetById(roleID));

        [HttpPost(nameof(CreateRole))]
        public IActionResult CreateRole(RoleModelDto role)
        {
            var result = _unitOfWork.Role.Insert(new EntityMapper().ToNewEntity(role));
            _unitOfWork.SaveAsync();
            if (result is not null) 
                return Ok("Role Created");
            else 
                return BadRequest("Error in Creating the role");
        }

        [HttpPut(nameof(UpdateRole))]
        public IActionResult UpdateRole(RoleModelDto role)
        {
            _unitOfWork.Role.Update(new EntityMapper().ToUpdateEntity(role));
            _unitOfWork.SaveAsync();
            return Ok("Role Updated");
        }

        [HttpDelete(nameof(DeleteRole))]
        public async Task<IActionResult> DeleteRole([FromQuery] int roleID)
        {
            await _unitOfWork.Role.Delete(roleID);
            return Ok("Role Deleted");
        }
      }
}
