using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper.Interface;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Members;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMembersBusiness _business;
        private readonly IMapper _mapper;
        private readonly IAmazonHelperService _aws;

        public MembersController(IMembersBusiness memberBusiness, IMapper mapper, IAmazonHelperService aws)
        {
            this._business = memberBusiness;
            this._mapper = mapper;
            this._aws = aws;
        }

        // GET: /Members
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
        [ProducesResponseType(typeof(IEnumerable<MemberResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status400BadRequest)]

        [HttpGet]    
        [Authorize(Roles = "Admin")]
        [Route("/Members")]
        public IActionResult GetAll() 
        {
            var members = _business.Find(c => c.IsDeleted == false);
            if (members == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<IEnumerable<MemberResponse>>(members));
        }

        // GET: /Members/5
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
        [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]

        [HttpGet]
        [Route("/Members/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _business.GetById(id);

            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<MemberResponse>(model));
        }

        // POST: /Members
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
        [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        [HttpPost]       
        [Route("/Members")]
        public async Task<IActionResult> Create([FromForm] MemberRequest model)
        {          
            if (model is null)
            {
                return BadRequest();
            }

            var url = AWSMockWithOutCredentials.UploadImage(model.ImageUrl);
            //var url = await _aws.UploadImage(model.Image);

            if (url == null)
            {
                return NotFound("File is required, to be uploaded.");
            }

            var newModel = new Member
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = url,
                InstagramUrl = model.InstagramUrl,
                LinkedInUrl = model.LinkedInUrl,
                FacebookUrl = model.FacebookUrl
            };

            var member = await _business.Insert(_mapper.Map<Member>(newModel));
            return Ok(_mapper.Map<MemberResponse>(member));
        }

        // GET: /Members/5
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
        [ProducesResponseType(typeof(MemberResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        [HttpPut]       
        [Route("/Members/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] MemberRequest model)
        {
            var member = await _business.GetById(id);

            if (member == null)
            {
                return NotFound("Not found");
            }
            var url = AWSMockWithOutCredentials.UploadImage(model.ImageUrl);
            //var url = await _aws.UploadImage(model.ImageUrl);

            if (url == null)
            {
                return NotFound("File is required, to be uploaded.");
            }

            member.Name = model.Name;
            member.Description = model.Description;
            member.ImageUrl = url;
            member.InstagramUrl = model.InstagramUrl;
            member.LinkedInUrl = model.LinkedInUrl;
            member.FacebookUrl = model.FacebookUrl;

            var memberResponse = await _business.Update(member);

            return Ok(_mapper.Map<MemberResponse>(memberResponse));
        }

        // Delete: /Members/5
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
        [Route("/Members/{id}")]
        public async Task<IActionResult> SoftDelete(int? id)
        {

            var member = await _business.GetById(id.Value);

            if (member == null)
            {
                return NotFound();
            }
            await _business.SoftDelete(member);
            await _business.Update(member);
            return Ok("Deleted successfully.");
        }
    }
}
