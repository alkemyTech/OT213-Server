using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs.Members;
using Microsoft.AspNetCore.Authorization;

namespace OngProject.Controllers
{
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberBusiness _memberBusiness;
        private readonly IMapper _mapper;
        public MemberController(IMemberBusiness memberBusiness, IMapper mapper)
        {
            this._memberBusiness = memberBusiness;
            this._mapper = mapper;
        }

        // GET List/Members
        [HttpGet]    
        [Authorize(Roles = "Admin")]
        [Route("List/Members")]
        public async Task<IActionResult> GetAllMembers() 
        {
            try
            {
                var members = _memberBusiness.Find(m => m.IsDeleted == false);
                return members != null ? Ok(_mapper.Map<IEnumerable<MemberGetModelDTO>>(members)) 
                                       : NotFound("The list of members has not been found");                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        // GET List/MemberById
        [HttpGet]        
        [Route("List/MemberById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if(string.IsNullOrEmpty(id.ToString()) || id == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        Status = "Error",
                        Message = "Please, set an ID."
                    }); 
                }

                var member = await _memberBusiness.GetById(id);
                return member != null ? Ok(_mapper.Map<MemberGetModelDTO>(member)) 
                                      : NotFound("Member doesn't exists");            
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST Create/Member
        [HttpPost]       
        [Route("Create/Member")]
        public async Task<IActionResult> Create([FromBody] MemberCreateModelDTO model)
        {          
            if(ModelState.IsValid)
            {
                try
                {
                    // validations                    
                    if(string.IsNullOrEmpty(model.name))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Name required"
                        });                    
                    }
                    if(string.IsNullOrEmpty(model.imageUrl))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Image required"
                        });
                    }

                    // request                    
                    await _memberBusiness.Insert(_mapper.Map<Member>(model));
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }            
            return Ok(new 
            {
                Status = "Success",
                Message = "Member creation successfully!"
            });                
        }

        // PUT Update/Member/{id}
        [HttpPut]       
        [Route("Update/Member/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] MemberUpdateModelDTO model)
        { 
            // validation
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
                    if(string.IsNullOrEmpty(model.name))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Name required"
                        });                   
                    }
                    if(string.IsNullOrEmpty(model.imageUrl))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Image required"
                        });
                    }

                    var member = await _memberBusiness.GetById(id);
                    if(member == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Member cannot be null."
                        });    
                    }

                    // Mapping and request
                    _mapper.Map(model,member);               
                    var updated = await _memberBusiness.Update(member);
                    if(updated == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Error updating data"
                        });    
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
                Message = "Member updated successfully!"
            }); 
        }

        // DELETE Delete/Member/{id}
        [HttpDelete]       
        [Route("Delete/Member/{id}")]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            // validation
            if(string.IsNullOrEmpty(id.ToString()) || id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "Please, set a valid ID."
                });  
            }
            try
            {
                var member = await _memberBusiness.GetById(id.Value);
                if(member == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        Status = "Error",
                        Message = "Member not found or doesn't exist."
                    });   
                }

                // request  
                await _memberBusiness.SoftDelete(member, id);
                await _memberBusiness.Update(member);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok(new 
            {
                Status = "Success",
                Message = "Member deleted successfully!"
            }); 
        }
    }
}
