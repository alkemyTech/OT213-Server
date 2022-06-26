using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper.Interface;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Testimonials;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialsBusiness _business;
        private readonly IMapper _mapper;
        private readonly IAmazonHelperService _aws;
        public TestimonialsController(ITestimonialsBusiness testimonialBusiness, IMapper mapper, IAmazonHelperService aws)
        {
            this._business = testimonialBusiness;
            this._mapper = mapper;
            this._aws = aws;
        }

        // GET: /Testimonials
        /// <summary>
        /// Obtiene todos los objetos.
        /// </summary>
        /// <remarks>
        /// Obtiene todos los objetos
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="200">OK.Devuelve los objetos solicitados.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(IEnumerable<TestimonialResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status400BadRequest)]

        [HttpGet]
        [Route("/Testimonials")]
        public IActionResult GetAll()
        {
            var testimonials = _business.Find(c => c.IsDeleted == false);
            if (testimonials == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<IEnumerable<TestimonialResponse>>(testimonials));
        }

        // GET: /Testimonials/5
        /// <summary>
        /// Obtiene un objeto por su Id.
        /// </summary>
        /// <remarks>
        /// Devuelve el objeto por su id si existe.
        /// </remarks>
        /// <param name="id">Id (int) del objeto.</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>        
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(TestimonialResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]

        [HttpGet]
        [Route("/Testimonials/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _business.GetById(id);

            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<TestimonialResponse>(model));
        }

        // POST: /Testimonials
        /// <summary>
        /// Crea un nuevo objeto en la BD.
        /// </summary>
        /// <remarks>
        /// Crea el objecto en la BD
        /// </remarks>
        /// <param name="model">Objeto a crear a la BD.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">Created. Objeto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(TestimonialResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]

        [HttpPost]
        [Route("/Testimonials")]
        public async Task<IActionResult> Create([FromForm] TestimonialRequest model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            var url = AWSMockWithOutCredentials.UploadImage(model.Image);
            //var url = await _aws.UploadImage(model.ImageUrl);

            if (url == null)
                return NotFound("File is required, to be uploaded.");

            var newModel = new Testimonial
            {
                Name = model.Name,
                Description = model.Description,
                Image = url
            };

            var testimonial = await _business.Insert(_mapper.Map<Testimonial>(newModel));
            return Ok(_mapper.Map<TestimonialResponse>(testimonial));
        }

        // GET: /Testimonials/5
        /// <summary>
        /// Obtiene un objeto por su Id.
        /// </summary>
        /// <remarks>
        /// Devuelve el objeto por su id si existe.
        /// </remarks>
        /// <param name="id">Id (int) del objeto.</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>        
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        [HttpPut]
        [Route("/Testimonials/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] TestimonialRequest model)
        {
            var testimonial = await _business.GetById(id);

            if (testimonial == null)
            {
                return NotFound("Not found");
            }
            var url = AWSMockWithOutCredentials.UploadImage(model.Image);
            //var url = await _aws.UploadImage(model.ImageUrl);

            if (url == null)
                return NotFound("File is required, to be uploaded.");

            testimonial.Name = model.Name;
            testimonial.Description = model.Description;
            testimonial.Image = url;

            var testimonialResponse = await _business.Update(testimonial);

            return Ok(_mapper.Map<TestimonialResponse>(testimonialResponse));
        }

        // Delete: /Testimonials/5
        /// <summary>
        /// Elimina un objeto por su Id.
        /// </summary>
        /// <remarks>
        /// Elimina el objeto por su id si existe.
        /// </remarks>
        /// <param name="id">Id (int) del objeto.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>      
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>        
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]

        [HttpDelete]
        [Route("/Testimonials/{id}")]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            var testimonial = await _business.GetById(id.Value);

            if (testimonial == null)
            {
                return NotFound();
            }
            await _business.SoftDelete(testimonial);
            await _business.Update(testimonial);
            return Ok("Deleted successfully.");
        }
    }
}