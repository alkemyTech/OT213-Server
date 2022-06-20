using AutoMapper;
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
            return Ok(_mapper.Map<IEnumerable<TestimonialGetDTO>>(testimonials));
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
            if (id != model.Id)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    Status = "Error",
                    Message = "Id number doesn't match!"
                });
            } 
           
            var testimonial = await _testimonialBusiness.GetById(id);       
            _mapper.Map(model, testimonial);
            await _testimonialBusiness.Update(_mapper.Map<Testimonial>(testimonial));
            return Ok($"{model.Name} testimonial Updated");
        }

        [HttpDelete]
        [Route("Delete/Testimonial/{id}")]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            var testimonial = await _testimonialBusiness.GetById(id);
            if (testimonial != null)
            {
                await _testimonialBusiness.SoftDelete(testimonial);
                await _testimonialBusiness.Update(testimonial);
                return Ok($"{testimonial.Name} testimonial Deleted");
            }
            return BadRequest("Error in deleting the testimonial");
        }
      }
}
