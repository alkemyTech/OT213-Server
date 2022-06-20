using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
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

        [HttpGet]    
        [Authorize(Roles = "Admin")]
        [Route("List/Members")]
        public IActionResult GetAllMembers() 
        {
            var members = _memberBusiness.Find(m => m.IsDeleted == false);
            return Ok(_mapper.Map<IEnumerable<MemberGetModelDTO>>(members)); 
        }

        [HttpGet]        
        [Route("List/MemberById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _memberBusiness.GetById(id);
            return Ok(_mapper.Map<MemberGetModelDTO>(member));
        }

        [HttpPost]       
        [Route("Create/Member")]
        public async Task<IActionResult> Create([FromBody] MemberCreateModelDTO model)
        {          
            await _memberBusiness.Insert(_mapper.Map<Member>(model));
            return Ok(new 
            {
                Status = "Success",
                Message = $"{model.name} member creation successfully!"
            });                
        }

        [HttpPut]       
        [Route("Update/Member/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] MemberUpdateModelDTO model)
        { 
            if (id != model.Id)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    Status = "Error",
                    Message = "Id number doesn't match!"
                });
            }  

            var member = await _memberBusiness.GetById(id);
            _mapper.Map(model,member);               
            await _memberBusiness.Update(member);                
            
            return Ok(new 
            {
                Status = "Success",
                Message = $"{model.name} member updated successfully!"
            }); 
        }

        [HttpDelete]       
        [Route("Delete/Member/{id}")]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            var member = await _memberBusiness.GetById(id.Value);   
            await _memberBusiness.SoftDelete(member);
            await _memberBusiness.Update(member);

            return Ok(new 
            {
                Status = "Success",
                Message = $"{member.Name} member deleted successfully!"
            }); 
        }
    }
}
