using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs;
using Microsoft.AspNetCore.Authorization;

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

        // GET List/Users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("All/Users")]
        public async Task<IActionResult> GetAllUsers()
        {            
            var users = _usersBusiness.Find(m => m.IsDeleted != true);
            return users != null ? Ok(_mapper.Map<IEnumerable<UsersDTO>>(users))
                                 : NotFound("The list of users has not been found");           
        } 

        // PUT Update/User/{id}
        [HttpPut]
        [Route("Update/User")]
        public async Task<IActionResult> Edit([FromBody] UsersDTO model)
        {          
            var user = await _usersBusiness.GetById(model.Id);
            if(user == null)
                return NotFound("User doesn't exists.");         

            _mapper.Map(model, user);
            var updatedUser = await _usersBusiness.Update(user);

            if(updatedUser == null)
                return BadRequest("Error updating data");                            
            
            return Ok(new
            {
                Status = "Success",
                Message = "User updated successfully!"
            });
        }

        // DELETE Delete/User/{id}
        [HttpDelete]
        [Route("Delete/User/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {         
            var user = await _usersBusiness.GetById(id.Value);
            if(user == null)
                return NotFound("User doesn't exists.");

            await _usersBusiness.SoftDelete(user, id);
            await _usersBusiness.Update(user);

            return Ok(new 
            {
                Status = "Success",
                Message = "User deleted successfully!"
            }); 
        }
    }
}
