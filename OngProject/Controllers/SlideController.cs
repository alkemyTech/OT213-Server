using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using System;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;

namespace OngProject.Controllers
{
    
    [ApiController]
    public class SlideController : ControllerBase
    {
        private readonly ISlidesBusiness _slideBusiness;
        private readonly IMapper _mapper;

        public SlideController(ISlidesBusiness slidesBusiness, IMapper mapper)
        {
            this._slideBusiness = slidesBusiness;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("/Slides")]
        public async Task<IActionResult> GetAllSlides()
        {
            try
            {
                var slides = _slideBusiness.Find(c => c.IsDeleted == false);
                return slides != null ? Ok(_mapper.Map<IEnumerable<SlideGetDTO>>(slides))
                                       : NotFound("The list of slides has not been found");
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [Route("/Slides/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest("Please, set an ID.");

                var slide = await _slideBusiness.GetById(id);
                return slide != null ? Ok(_mapper.Map<SlideDetailsDTO>(slide))
                                      : NotFound("Slide doesn't exists");
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("/Slides")]
        public async Task<IActionResult> Create([FromBody] SlideCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // request                    
                    await _slideBusiness.Insert(_mapper.Map<Slide>(model));
                }
                catch (System.Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return Ok(new
            {
                Status = "Success",
                Message = "Slide creation successfully!"
            });
        }


        [HttpPut]
        [Route("/Slides/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] SlideUpdateDTO model)
        {
            if (id != model.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "Id number not found!"
                });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var slide = await _slideBusiness.GetById(id);
                    if (slide == null)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, new
                        {
                            Status = "Error",
                            Message = "Slide cannot be null."
                        });
                    }

                    // Mapping and request
                    _mapper.Map(model, slide);
                    var updated = await _slideBusiness.Update(slide);
                    if (updated != null)
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
                Message = "Slide updated successfully!"
            });
        }


        [HttpDelete]
        [Route("/Slides/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            // validation
            if (id == 0)
                return BadRequest("Please, set a valid ID.");

            try
            {
                var slide = await _slideBusiness.GetById(id.Value);

                if (slide == null)
                    return NotFound("Slide not found or doesn't exist.");

                await _slideBusiness.SoftDelete(slide, id);
                await _slideBusiness.Update(slide);

                return Ok("Slide deleted successfully.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}
