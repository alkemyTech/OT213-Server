using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Mapper;
using OngProject.Core.Models.DTOs.Testimonial;
using OngProject.DataAccess.UnitOfWork.Interfaces;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly ITestimonialBusiness _testimonialBusiness;
        private readonly IMapper _mapper;
        public TestimonialController(ITestimonialBusiness testimonialBusiness, IMapper mapper)
        {
            this._testimonialBusiness = testimonialBusiness;
            this._mapper = mapper;
        }

        [HttpGet(nameof(GetTestimonials))] 
        public async Task<IActionResult> GetTestimonials() => Ok(
            _mapper.Map<IEnumerable<TestimonialGetDTO>>(
                _testimonialBusiness.Find(m => m.IsDeleted == false)
                )
            );

        [HttpGet(nameof(GetTestimonialById))]
        public async Task<IActionResult> GetTestimonialById([FromQuery] int testimonialID) => Ok(
            _mapper.Map<TestimonialGetDTO>(
                await _testimonialBusiness.GetById(testimonialID)
                )
            );

        [HttpPost(nameof(CreateTestimonial))]
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

        [HttpPut(nameof(UpdateTestimonial))]
        public IActionResult UpdateTestimonial(TestimonialUpdateDTO testimonial)
        {
            if (ModelState.IsValid)
            {
                _testimonialBusiness.Update(_mapper.Map<Testimonial>(testimonial));
                return Ok("Testimonial Updated");
            }
            return BadRequest("Error in Creating the testimonial");
        }

        [HttpDelete(nameof(DeleteTestimonial))]
        public async Task<IActionResult> DeleteTestimonial([FromQuery] int testimonialID)
        {
            var testimonial = await _testimonialBusiness.GetById(testimonialID);
            if (testimonial == null)
            {
                await _testimonialBusiness.SoftDelete(testimonial, testimonialID);
                await _testimonialBusiness.Update(testimonial);
                return Ok("Testimonial Deleted");
            }
            return BadRequest("Error in deleting the testimonial");
        }
      }
}
