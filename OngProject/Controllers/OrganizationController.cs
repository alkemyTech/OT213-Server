using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs.Organizations;
using Microsoft.AspNetCore.Authorization;

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
            return orgs != null 
                        ? Ok(_mapper.Map<IEnumerable<OrganizationGetDTO>>(orgs))
                        : NotFound("The list of organizations have not been found");
        }
        
        [HttpPost]
        [Route("Create/Organization")]
        public async Task<IActionResult> Create([FromBody] OrganizationCreateDTO organizationCreateDTO)
        {
            await _organizationBusiness.Insert(_mapper.Map<Organization>(organizationCreateDTO));            
            return Ok(new
            {
                Status = "Success",
                Message = $"{organizationCreateDTO.Name} organization created successfully!"
            });
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Update/Organization/Public/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] OrganizationUpdateDTO organizationUpdateDTO)
        {            
            var org = await _organizationBusiness.GetById(id);            
            _mapper.Map(organizationUpdateDTO, org);
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
