using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using System;

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
        public IActionResult GetAllUsers()
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
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "User not found or doesn't exist."
                });   
            }      

            try
            {
                _mapper.Map(model, user);
                var updatedUser = await _usersBusiness.Update(user);                
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating data. {ex.Message}");
            }                                            
            
            return Ok(new
            {
                Status = "Success",
                Message = $"{model.FirstName +" "+ model.LastName} updated successfully!"
            });
        }

        // DELETE Delete/User/{id}
        [HttpDelete]
        [Route("Delete/User/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {        
            var user = await _usersBusiness.GetById(id.Value);
            if(user == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "User not found or doesn't exist."
                });   
            }

            try
            {
                await _usersBusiness.SoftDelete(user);
                await _usersBusiness.Update(user);                
            }
            catch (Exception ex)
            {
                return BadRequest($"The user cannot be deleted. Probably failed access to the database. \n {ex.Message}");
            }

            return Ok(new 
            {
                Status = "Success",
                Message = "User deleted successfully!"
            }); 
        }
    }
}
