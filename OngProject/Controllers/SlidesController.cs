using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper.Interface;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Slides;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{

    [ApiController]
    [Authorize(Roles = "Admin")]      
    public class SlidesController : ControllerBase
    {
        private readonly ISlidesBusiness _business;
        private readonly IMapper _mapper;
        private readonly IAmazonHelperService _aws;

        public SlidesController(ISlidesBusiness slidesBusiness, IMapper mapper, IAmazonHelperService aws)
        {
            this._business = slidesBusiness;
            this._mapper = mapper;
            this._aws = aws;
        }

        // GET: /Slides
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
        [ProducesResponseType(typeof(IEnumerable<SlideResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status400BadRequest)]

        [HttpGet]
        [Route("/Slides")]
        public  IActionResult GetAll()
        {
            var slides = _business.Find(c => c.IsDeleted == false);
            if (slides == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<IEnumerable<SlideResponse>>(slides));
        }

        // GET: /Slides/5
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
        [ProducesResponseType(typeof(SlideResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]

        [HttpGet]
        [Route("/Slides/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _business.GetById(id);

            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<SlideResponse>(model));
        }

        // POST: /Slides
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
        /// <response code = "404" > NotFound.No se ha encontrado el objeto solicitado.</response>    
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(SlideResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        [HttpPost]
        [Route("/Slides")]
        public async Task<IActionResult> Create([FromForm] SlideRequest model)
        {
            if (model is null)
            {
                return BadRequest("Bad request");
            }
            if (model.OrganizationId == 0) {
                return BadRequest("OrganizationId cannot be null");
            }
            var url = AWSMockWithOutCredentials.UploadImage(model.ImageUrl);
            //var url = await _aws.UploadImage(model.ImageUrl);

            if (url == null)
                return NotFound("File is required, to be uploaded.");

            var newModel = new Slide
            {
                Name = model.Name,
                Text = model.Text,
                ImageUrl = url,
                Order = model.Order,
                OrganizationId = model.OrganizationId
            };

            var slide= await _business.Insert(_mapper.Map<Slide>(newModel));
            return Ok(_mapper.Map<SlideResponse>(slide));
        }

        // GET: /Slides/5
        /// <summary>
        /// Obtiene un objeto por su Id.
        /// </summary>
        /// <remarks>
        /// Devuelve el objeto por su id si existe.
        /// </remarks>
        /// <param name="id">Id (int) del objeto.</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code = "404" > NotFound.No se ha encontrado el objeto solicitado.</response>    
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(SlideResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        [HttpPut]
        [Route("/Slides/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] SlideRequest model)
        {
            var slide = await _business.GetById(id);

            if (slide == null)
            {
                return NotFound("Not found");
            }

            var url = AWSMockWithOutCredentials.UploadImage(model.ImageUrl);
            //var url = await _aws.UploadImage(model.ImageUrl);

            if (url == null)
                return NotFound("File is required, to be uploaded.");

            slide.Name = model.Name;
            slide.Text = model.Text;
            slide.ImageUrl = url;
            slide.Order = model.Order;
            slide.OrganizationId = model.OrganizationId;

            var slideResponse = await _business.Update(slide);

            return Ok(_mapper.Map<SlideResponse>(slideResponse));
        }

        // Delete: /Slides/5
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
        [Route("/Slides/{id}")]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            var slide = await _business.GetById(id.Value);

            if (slide == null)
            {
                return NotFound();
            }
            await _business.SoftDelete(slide);
            await _business.Update(slide);
            return Ok("Deleted successfully.");
        }
    }
}