using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
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
        public async Task<IActionResult> GetAllMembers() => Ok(await _uow.Members.GetAll());

        // GET List/MemberById
        [HttpGet]        
        [Route("List/MemberById")]
        public async Task<IActionResult> GetById(int id)
        {
            if(id == 0)
                return NotFound("Please, set an ID.");

            var member = await _uow.Members.GetById(id);
            return member != null ? Ok(member) : NotFound("Member doesn't exists");
        }

        // POST Create/Member
        [HttpPost]       
        [Route("Create/Member")]
        public async Task<IActionResult> Create(Member member)
        {
            if(ModelState.IsValid)
            {
                member = await _uow.Members.Insert(member);
                await _uow.SaveAsync();
            }
            return Ok(member);
        }

        // PUT Update/Member
        [HttpPut]       
        [Route("Update/Member/{id}")]
        public async Task<IActionResult> Edit(Member member)
        {
            if(ModelState.IsValid)
            {
                member = await _uow.Members.Update(member);
                await _uow.SaveAsync();
            }
            return Ok(member);
        }

        // DELETE Delete/Member
        [HttpDelete]       
        [Route("Delete/Member/{id}")]
        public async Task<IActionResult> Delete(Member member, int? id)
        {
            try
            {
                if(id == null)
                    return NotFound("Member not found");

                await _memberBusiness.SoftDelete(member, id);
                await _uow.SaveAsync();

                return Ok("Member deleted successfully.");
            }
            catch (Exception)
            {
                return NotFound("Member doesn't exists");
            }
        }
    }
}
