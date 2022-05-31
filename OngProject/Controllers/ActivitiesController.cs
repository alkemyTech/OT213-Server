using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs.Activities;
    
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
        public async Task<IActionResult> GetAllOrganizations()
        {
            try
            {
                var activities = _activitiesBusiness.Find(m => m.IsDeleted == false);
                return activities != null
                                  ? Ok(_mapper.Map<IEnumerable<ActivitiesGetDTO>>(activities))
                                  : NotFound("The list of activities have not been found");
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("List/ActivityById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString()) || id == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        Status = "Error",
                        Message = "Please, set an ID."
                    });
                }

                var activity = await _activitiesBusiness.GetById(id);
                return activity != null
                           ? Ok(_mapper.Map<ActivitiesGetDTO>(activity))
                           : NotFound("Activity doesn't exists");
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("Create/Activity")]
        public async Task<IActionResult> Create([FromBody] ActivityCreateDTO activityCreateDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _activitiesBusiness.Insert(_mapper.Map<Activities>(activityCreateDTO));
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return Ok(new
            {
                Status = "Success",
                Message = "Activity created successfully!"
            });
        }


        [HttpPut]
        [Route("Update/Activity/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ActivityUpdateDTO activityUpdateDTO)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var activity = await _activitiesBusiness.GetById(id);
                    if (activity == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Activity cannot be null."
                        });
                    }

                    _mapper.Map(activityUpdateDTO, activity);
                    var updated = await _activitiesBusiness.Update(activity);
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
                Message = "Activity updated successfully!"
            });
        }


        [HttpDelete]
        [Route("Delete/Activity/{id}")]
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
                var activity = await _activitiesBusiness.GetById(id.Value);
                if (activity == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        Status = "Error",
                        Message = "Activity not found or doesn't exist."
                    });
                }

                await _activitiesBusiness.SoftDelete(activity, id);
                activity.DeletedAt = DateTime.Now;
                await _activitiesBusiness.Update(activity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Ok(new
            {
                Status = "Success",
                Message = "Activity deleted successfully!"
            });
        }

    }
}
