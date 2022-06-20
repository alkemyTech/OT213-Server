using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs.Activities;
using Microsoft.AspNetCore.Authorization;

namespace OngProject.Controllers
{
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesBusiness _activitiesBusiness;
        private readonly IMapper _mapper;
        public ActivitiesController(IActivitiesBusiness activitiesBusiness, IMapper mapper)
        {
            this._activitiesBusiness = activitiesBusiness;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("List/Activities")]
        public  IActionResult GetAllOrganizations()
        {
            var activities = _activitiesBusiness.Find(m => m.IsDeleted == false);
            return activities != null
                                ? Ok(_mapper.Map<IEnumerable<ActivitiesGetDTO>>(activities))
                                : NotFound("The list of activities have not been found");
        }

        [HttpGet]
        [Route("List/ActivityById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var activity = await _activitiesBusiness.GetById(id);
            return activity != null
                        ? Ok(_mapper.Map<ActivitiesGetDTO>(activity))
                        : NotFound($"{activity.Name} activity doesn't exists");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Create/Activity")]
        public async Task<IActionResult> Create([FromBody] ActivityCreateDTO activityCreateDTO)
        {
            await _activitiesBusiness.Insert(_mapper.Map<Activities>(activityCreateDTO));
            return Ok(new
            {
                Status = "Success",
                Message = $"{activityCreateDTO.Name} activity created successfully!"
            });
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("Update/Activity/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ActivityUpdateDTO activityUpdateDTO)
        {
            var activity = await _activitiesBusiness.GetById(id);
            _mapper.Map(activityUpdateDTO, activity);
            var updated = await _activitiesBusiness.Update(activity);
            
            return Ok(new
            {
                Status = "Success",
                Message = $"{activity.Name} activity updated successfully!"
            });
        }

        [HttpDelete]
        [Route("Delete/Activity/{id}")]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            var activity = await _activitiesBusiness.GetById(id.Value);
            await _activitiesBusiness.SoftDelete(activity);
            activity.DeletedAt = DateTime.Now;
            await _activitiesBusiness.Update(activity);
           
            return Ok(new
            {
                Status = "Success",
                Message = $"{activity.Name} activity deleted successfully!"
            });
        }

    }
}
