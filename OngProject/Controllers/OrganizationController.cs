using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System;
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
        public async Task<IActionResult> GetAllOrganizationsPublic()
        {
            try
            {
                var orgs = _organizationBusiness.Find(m => m.IsDeleted == false);
                return orgs != null 
                            ? Ok(_mapper.Map<IEnumerable<OrganizationGetDTO>>(orgs))
                            : NotFound("The list of organizations have not been found");
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        [HttpPost]
        [Route("Create/Organization")]
        public async Task<IActionResult> Create([FromBody] OrganizationCreateDTO organizationCreateDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {   
                    await _organizationBusiness.Insert(_mapper.Map<Organization>(organizationCreateDTO));
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return Ok(new
            {
                Status = "Success",
                Message = "Organization created successfully!"
            });
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Update/Organization/Public/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] OrganizationUpdateDTO organizationUpdateDTO)
        {            
            if (ModelState.IsValid)
            {
                try
                {                    
                    var org = await _organizationBusiness.GetById(id);
                    if (org == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Organization cannot be null."
                        });
                    }

                    _mapper.Map(organizationUpdateDTO, org);
                    var updated = await _organizationBusiness.Update(org);
                    if (updated == null)
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
                Message = "Organization updated successfully!"
            });
        }


        [HttpDelete]
        [Route("Delete/Organization/{id}")]
        public async Task<IActionResult> SoftDelete(int? id)
        {            
            if (string.IsNullOrEmpty(id.ToString()) || id == 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "Please, set a valid ID."
                });
            }
            try
            {
                var org = await _organizationBusiness.GetById(id.Value);
                if (org == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        Status = "Error",
                        Message = "Organization not found or doesn't exist."
                    });
                }
               
                await _organizationBusiness.SoftDelete(org);
                await _organizationBusiness.Update(org);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok(new
            {
                Status = "Success",
                Message = "Organization deleted successfully!"
            });
        }

    }
}
