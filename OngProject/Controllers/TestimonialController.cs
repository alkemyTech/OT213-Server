﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Testimonial;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]      
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialBusiness _testimonialBusiness;
        private readonly IMapper _mapper;
        public TestimonialController(ITestimonialBusiness testimonialBusiness, IMapper mapper)
        {
            this._testimonialBusiness = testimonialBusiness;
            this._mapper = mapper;
        }

        [HttpGet]
        [Route("List/Testimonials")]
        public IActionResult GetTestimonials() 
        { 
            var testimonials = _testimonialBusiness.Find(t => t.IsDeleted == false);
            return testimonials != null ? Ok(_mapper.Map<IEnumerable<TestimonialGetDTO>>(testimonials))
                                        : NotFound("The list of testimonials has not been found");
        }

        [HttpGet]
        [Route("List/TestimonialById/{id}")]
        public async Task<IActionResult> GetTestimonialById(int id) {
            return Ok(_mapper.Map<TestimonialGetDTO>(await _testimonialBusiness.GetById(id)));
        }

        [HttpPost]
        [Route("Create/Testimonial")]
        public async Task<IActionResult> CreateTestimonial(TestimonialCreateDTO testimonial)
        {
            if (ModelState.IsValid)
            {
                await _testimonialBusiness.Insert(_mapper.Map<Testimonial>(testimonial));
                return Ok("Testimonial Created");
            }
            else 
                return BadRequest("Error in Creating the testimonial");
        }

        [HttpPut]
        [Route("Update/Testimonial/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] TestimonialUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var testimonial = await _testimonialBusiness.GetById(id);
                if (testimonial == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new
                    {
                        Status = "Error",
                        Message = "Testimonial cannot be null."
                    });
                }
                _ = _testimonialBusiness.Update(_mapper.Map<Testimonial>(model));
                return Ok("Testimonial Updated");
            }
            return BadRequest("Error in Creating the testimonial");
        }

        [HttpDelete]
        [Route("Delete/Testimonial/{id}")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var testimonial = await _testimonialBusiness.GetById(id);
            if (testimonial == null)
            {
                await _testimonialBusiness.SoftDelete(testimonial);
                await _testimonialBusiness.Update(testimonial);
                return Ok("Testimonial Deleted");
            }
            return BadRequest("Error in deleting the testimonial");
        }
      }
}
