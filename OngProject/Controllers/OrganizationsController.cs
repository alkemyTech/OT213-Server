using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper.Interface;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Organizations;
using OngProject.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    public class OrganizationsController : ControllerBase
    {
        private readonly IOrganizationsBusiness _business;
        private readonly IMapper _mapper;
        private readonly IAmazonHelperService _aws;
        public OrganizationsController(IOrganizationsBusiness organizationBusiness, IMapper mapper, IAmazonHelperService aws)
        {
            this._business = organizationBusiness;
            this._mapper = mapper;
            this._aws = aws;
        }

        // GET: /Organizations
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
        [ProducesResponseType(typeof(IEnumerable<OrganizationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status400BadRequest)]

        [HttpGet]
        [Route("/Organizations")]
        public  IActionResult GetAll()
        {
            var organizations = _business.Find(c => c.IsDeleted == false);
            if (organizations == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<IEnumerable<OrganizationResponse>>(organizations));
        }

        // GET: /Organizations/5
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
        [ProducesResponseType(typeof(OrganizationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]

        [HttpGet]
        [Route("/Organizations/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _business.GetById(id);

            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<OrganizationResponse>(model));
        }

        // POST: /Organizations
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
        [ProducesResponseType(typeof(OrganizationResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        [HttpPost]
        [Route("/Organizations")]
        public async Task<IActionResult> Create([FromForm] OrganizationRequest model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            var url = AWSMockWithOutCredentials.UploadImage(model.Image);
            //var url = await _aws.UploadImage(model.Image);

            if (url == null)
            {
                return NotFound("File is required, to be uploaded.");
            }

            var newModel = new Organization
            {
                Name = model.Name,
                AboutUs = model.AboutUs,
                Address = model.Address,
                Email = model.Email,
                Welcome = model.Welcome,
                Phone = model.Phone,
                FacebookUrl = model.FacebookUrl,
                InstagramUrl = model.InstagramUrl,
                LinkedInUrl = model.LinkedInUrl,
                Image = url,
            };

            var organization = await _business.Insert(_mapper.Map<Organization>(newModel));
            return Ok(_mapper.Map<OrganizationResponse>(organization)); ;
        }

        // GET: /Organizations/5
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
        [ProducesResponseType(typeof(OrganizationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("/Organizations/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] OrganizationRequest model)
        {
            var organization = await _business.GetById(id);

            if (organization == null)
            {
                return NotFound("Not found");
            }

            var url = AWSMockWithOutCredentials.UploadImage(model.Image);
            //var url = await _aws.UploadImage(model.Image);

            if (url == null)
            {
                return NotFound("File is required, to be uploaded.");
            }

            organization.Name = model.Name;
            organization.AboutUs = model.AboutUs;
            organization.Address = model.Address;
            organization.Email = model.Email;
            organization.Welcome = model.Welcome;
            organization.Phone = model.Phone;
            organization.FacebookUrl = model.FacebookUrl;
            organization.InstagramUrl = model.InstagramUrl;
            organization.LinkedInUrl = model.LinkedInUrl;
            organization.Image = url;

            var organizationResponse = await _business.Update(organization);

            return Ok(_mapper.Map<OrganizationResponse>(organizationResponse));

        }
        // Delete: /Organizations/5
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
        [Route("/Organizations/{id}")]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            var organization = await _business.GetById(id.Value);

            if (organization == null)
            {
                return NotFound();
            }
            await _business.SoftDelete(organization);
            await _business.Update(organization);
            return Ok("Deleted successfully.");
        }
    }
}
