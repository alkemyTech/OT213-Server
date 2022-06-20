using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs.Organizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace OngProject.Controllers
{
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationBusiness _organizationBusiness;
        private readonly IMapper _mapper;
        public OrganizationController(IOrganizationBusiness organizationBusiness, IMapper mapper)
        {
            this._organizationBusiness = organizationBusiness;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("List/Organization/Public")]
        public  IActionResult GetAllOrganizationsPublic()
        {
            var orgs = _organizationBusiness.Find(m => m.IsDeleted == false);
            return Ok(_mapper.Map<IEnumerable<OrganizationGetDTO>>(orgs));
        }
        
        [HttpPost]
        [Route("Create/Organization")]
        public async Task<IActionResult> Create([FromBody] OrganizationCreateDTO model)
        {
            await _organizationBusiness.Insert(_mapper.Map<Organization>(model));            
            return Ok(new
            {
                Status = "Success",
                Message = $"{model.Name} organization created successfully!"
            });
        }
        
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("Update/Organization/Public/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] OrganizationUpdateDTO model)
        {   
            if (id != model.Id)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    Status = "Error",
                    Message = "Id number doesn't match!"
                });
            }   
                   
            var org = await _organizationBusiness.GetById(id);            
            _mapper.Map(model, org);
            var updated = await _organizationBusiness.Update(org);
            
            return Ok(new
            {
                Status = "Success",
                Message = $"{org.Name} organization updated successfully!"
            });
        }

        [HttpDelete]
        [Route("Delete/Organization/{id}")]
        public async Task<IActionResult> SoftDelete(int? id)
        {            
            var org = await _organizationBusiness.GetById(id.Value);
            await _organizationBusiness.SoftDelete(org);
            await _organizationBusiness.Update(org);
            
            return Ok(new
            {
                Status = "Success",
                Message = $"{org.Name} organization deleted successfully!"
            });
        }

    }
}
