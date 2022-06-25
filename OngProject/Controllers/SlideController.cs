using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Entities;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs.Slides;
using Microsoft.AspNetCore.Authorization;
using OngProject.Core.Helper.Interface;

namespace OngProject.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]      
    public class SlideController : ControllerBase
    {
        private readonly ISlidesBusiness _slideBusiness;
        private readonly IMapper _mapper;
        private readonly IAmazonHelperService _aws;


        public SlideController(ISlidesBusiness slidesBusiness, IMapper mapper, IAmazonHelperService aws)
        {
            this._slideBusiness = slidesBusiness;
            this._mapper = mapper;
            this._aws = aws;
        }

        [HttpGet]
        [Route("/Slide/All")]
        public  IActionResult GetAllSlides()
        {
            var slides = _slideBusiness.Find(c => c.IsDeleted == false);
            return Ok(_mapper.Map<IEnumerable<SlideGetDTO>>(slides));
        }

        [HttpGet]
        [Route("/Slide/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var slide = await _slideBusiness.GetById(id);
            return Ok(_mapper.Map<SlideDetailsDTO>(slide));
        }

        [HttpPost]
        [Route("/Slide/Create")]
        public async Task<IActionResult> Create([FromForm] SlideCreateDTO model)
        {
            if(model.OrganizationId == 0)
                return BadRequest("OrganizationId cannot be null");

            var url = await _aws.UploadImage(model.ImageUrl);
            if(url == null)
                return NotFound("File is required, to be uploaded.");

            var getSlide = new Slide
            {
                Name = model.Name,
                Text = model.Text,
                ImageUrl = url,
                Order = model.Order,
                OrganizationId = model.OrganizationId
            };

            await _slideBusiness.Insert(_mapper.Map<Slide>(getSlide));
            return Ok(new
            {
                Status = "Success",
                Message = $"{model.Name} slide creation successfully!"
            });
        }

        [HttpPut]
        [Route("/Slides/Update/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] SlideUpdateDTO model)
        {
            if (id != model.Id)
            {
                return StatusCode(StatusCodes.Status404NotFound, new
                {
                    Status = "Error",
                    Message = "Id number doesn't match!"
                });
            }  
                
            var slide = await _slideBusiness.GetById(id);          
            _mapper.Map(model, slide);
            await _slideBusiness.Update(slide);
            
            return Ok(new
            {
                Status = "Success",
                Message = $"{model.Name} slide updated successfully!"
            });
        }

        [HttpDelete]
        [Route("/Slide/Delete/{id}")]
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
