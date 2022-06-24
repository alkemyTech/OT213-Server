using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace OngProject.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBusiness _usersBusiness;
        private readonly IMapper _mapper;
        public UsersController(IUsersBusiness usersBusiness, IMapper mapper)
        {
            this._usersBusiness = usersBusiness;
            this._mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("All/Users")]
        public IActionResult GetAllUsers()
        {            
            var users = _usersBusiness.Find(m => m.IsDeleted != true);
            return Ok(_mapper.Map<IEnumerable<UsersDTO>>(users));
        } 

        [HttpPut]
        [Route("Update/User/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UserUpdateDTO model)
        {          
            if (id != model.Id)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    Status = "Error",
                    Message = "Id number doesn't match!"
                });
            } 

            var user = await _usersBusiness.GetById(model.Id);    
            _mapper.Map(model, user);
            await _usersBusiness.Update(user);              
            
            return Ok(new
            {
                Status = "Success",
                Message = $"{model.FirstName +" "+ model.LastName} updated successfully!"
            });
        }

        [HttpDelete]
        [Route("Delete/User/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {        
            var user = await _usersBusiness.GetById(id.Value);           
            await _usersBusiness.SoftDelete(user);
            await _usersBusiness.Update(user);              

            return Ok(new 
            {
                Status = "Success",
                Message = $"{user.FirstName +" "+ user.LastName} user deleted successfully!"
            }); 
        }
    }
}
