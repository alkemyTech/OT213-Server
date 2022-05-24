using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using System;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMemberBusiness _memberBusiness;
        public MemberController(IUnitOfWork uow, IMemberBusiness memberBusiness)
        {
            this._uow = uow;
            this._memberBusiness = memberBusiness;
        }

        // GET List/Members
        [HttpGet]    
        [Route("List/Members")]
        public async Task<IActionResult> GetAllMembers() 
        {
            try
            {
                var members = _uow.Members.Find(m => m.isDeleted != true);
                if(members != null)
                {
                    return Ok(members);
                }
                return NotFound("The list of members has not been found");                
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }           
        }

        // GET List/MemberById
        [HttpGet]        
        [Route("List/MemberById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if(id == 0)
                    return NotFound("Please, set an ID.");

                var member = await _uow.Members.GetById(id);
                return member != null ? Ok(member) : NotFound("Member doesn't exists");            
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST Create/Member
        [HttpPost]       
        [Route("Create/Member")]
        public async Task<IActionResult> Create(MemberCreateModelDTO model)
        {            
            var member = new Member
            {
                Name = model.name,
                FacebookUrl = model.facebookUrl,
                InstagramUrl = model.instagramUrl,
                LinkedInUrl = model.linkedInUrl,
                ImageUrl = model.imageUrl,
                Description = model.description,
                CreatedAt = model.createdAt,
                UpdatedAt = model.updatedAt
            };

            if(ModelState.IsValid)
            {
                try
                {
                    // validations                    
                    if(string.IsNullOrEmpty(model.name))
                    {
                        return Ok("Name required");                    
                    }
                    if(string.IsNullOrEmpty(model.imageUrl))
                    {
                        return Ok("Image required");
                    }

                    // request
                    await _uow.Members.Insert(member);
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
                Message = "Member creation successfully!"
            });                
        }

        // PUT Update/Member/{id}
        [HttpPut]       
        [Route("Update/Member/{id}")]
        public async Task<IActionResult> Edit(MemberUpdateModelDTO model)
        {
            // var mem = _uow.Members.Find(c => c.MembersID == model.membersID);

            // var member = new Member
            // {
            //     Name = model.name,
            //     FacebookUrl = model.facebookUrl,
            //     InstagramUrl = model.imageUrl,
            //     LinkedInUrl = model.linkedInUrl,
            //     ImageUrl = model.imageUrl,
            //     Description = model.description,
            //     CreatedAt = model.createdAt,
            //     UpdatedAt = model.updatedAt,
            //     isDeleted = model.isDeleted
            // };

            // if(ModelState.IsValid)
            // {
            //     try
            //     {
            //         // validations

            //         // request
            //         if(mem != null)
            //         {
            //             member = await _uow.Members.Update(member);
            //             await _uow.SaveAsync();                  
            //         }
            //     }
            //     catch (System.Exception ex)
            //     {
            //         throw new Exception(ex.Message);
            //     }
            // }
            return Ok(new 
            {
                Status = "Success",
                Message = "Member updated successfully!"
            }); 
        }

        // DELETE Delete/Member/{id}
        [HttpDelete]       
        [Route("Delete/Member/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                var member = await _uow.Members.GetById(id.Value);

                if(id == null)
                    return NotFound("Member not found");

                await _memberBusiness.SoftDelete(member, id);
                await _uow.Members.Update(member);
                await _uow.SaveAsync();

                return Ok("Member deleted successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
