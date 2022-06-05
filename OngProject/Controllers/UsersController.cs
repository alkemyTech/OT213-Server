using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using System;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs;
using OngProject.Core.Models.DTOs.Members;

namespace OngProject.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IUsersBusiness _usersBusiness;
        private readonly IMapper _mapper;
        public UsersController(IUnitOfWork uow, IUsersBusiness usersBusiness, IMapper mapper)
        {
            this._uow = uow;
            this._usersBusiness = usersBusiness;
            this._mapper = mapper;
        }

        // GET List/Users
        [HttpGet]    
        [Route("List/Users")]
        public async Task<IActionResult> GetAllUsers() 
        {
            try
            {
                var users = _usersBusiness.FindUsersAsync(m => m.softDelete != true);
                return users != null ? Ok(_mapper.Map<IEnumerable<UsersDTO>>(users)) 
                                       : NotFound("The list of users has not been found");                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        // GET List/UserById
        [HttpGet]        
        [Route("List/UserById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if(id == 0)
                    return NotFound("Please, set an ID.");

                var user = await _usersBusiness.GetUsersByIdAsync(id);
                return user != null ? Ok(_mapper.Map<MemberGetModelDTO>(user)) 
                                      : NotFound("User doesn't exists");            
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST Create/User
        [HttpPost]       
        [Route("Create/User")]
        public async Task<IActionResult> Create([FromBody] UsersDTO model)
        {          
            if(ModelState.IsValid)
            {
                try
                {
                    // validations                    
                    if(string.IsNullOrEmpty(model.FirstName))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Name is required"
                        });
                    }
                    if(string.IsNullOrEmpty(model.Photo))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Photo is required"
                        });
                    }

                    // request                    
                    await _usersBusiness.InsertUsersAsync(_mapper.Map<User>(model));
                    await _uow.SaveAsync();                        
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }            
            return Ok(new 
            {
                Status = "Success",
                Message = "User creation successfully!"
            });                
        }

        // PUT Update/User/{id}
        [HttpPut]       
        [Route("Update/User/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UsersDTO model)
        { 
            if (id != model.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "Id number not found!"
                });
            }  

            if(ModelState.IsValid)
            {
                try
                {
                    // validations                    
                    if(string.IsNullOrEmpty(model.FirstName))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Name is required"
                        });
                    }
                    if(string.IsNullOrEmpty(model.Photo))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Photo is required"
                        });
                    }

                    var user = await _usersBusiness.GetUsersByIdAsync(id);
                    var updatedUser = await _usersBusiness.UpdateUsersAsync(user);

                    if(updatedUser != null)
                    {
                        // request    
                        _mapper.Map(model, user);               
                        await _uow.SaveAsync();           
                    }
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
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
            // validation
            if(id == 0)
                return NotFound("Please, set a valid ID.");

            try
            {
                var user = await _usersBusiness.GetUsersByIdAsync(id.Value);

                if(user == null)
                    return NotFound("User not found or doesn't exist.");

                await _usersBusiness.SoftDelete(user, id);
                await _usersBusiness.UpdateUsersAsync(user);
                await _uow.SaveAsync();

                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
