using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs.Slides;
using Microsoft.AspNetCore.Authorization;

namespace OngProject.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]      
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
        public  IActionResult GetAllSlides()
        {
            var slides = _slideBusiness.Find(c => c.IsDeleted == false);
            return slides != null ? Ok(_mapper.Map<IEnumerable<SlideGetDTO>>(slides))
                                  : NotFound("The list of slides has not been found");
        }

        [HttpGet]
        [Route("/Slides/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var slide = await _slideBusiness.GetById(id);
            return slide != null ? Ok(_mapper.Map<SlideDetailsDTO>(slide))
                                 : NotFound($"{slide.Name} slide doesn't exists");
        }

        [HttpPost]
        [Route("/Slides")]
        public async Task<IActionResult> Create([FromBody] SlideCreateDTO model)
        {
            await _slideBusiness.Insert(_mapper.Map<Slide>(model));
            return Ok(new
            {
                Status = "Success",
                Message = $"{model.Name} slide creation successfully!"
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
                    Message = "Id number doesn't match!"
                });
            }           
                
            var slide = await _slideBusiness.GetById(id);          
            _mapper.Map(model, slide);
            var updated = await _slideBusiness.Update(slide);
            
            return Ok(new
            {
                Status = "Success",
                Message = $"{model.Name} slide updated successfully!"
            });
        }

        [HttpDelete]
        [Route("/Slides/{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            var slide = await _slideBusiness.GetById(id.Value);    
            await _slideBusiness.SoftDelete(slide);
            await _slideBusiness.Update(slide);

            return Ok(new
            {
                Status = "Success",
                Message = $"{slide.Name} slide deleted successfully!"
            });
        }

    }

}
