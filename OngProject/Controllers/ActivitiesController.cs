using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Helper.Interface;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Activities;
using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Controllers
{
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly IActivitiesBusiness _business;
        private readonly IMapper _mapper;
        private readonly IAmazonHelperService _aws;
        public ActivitiesController(IActivitiesBusiness activitiesBusiness, IMapper mapper, IAmazonHelperService aws)
        {
            this._business = activitiesBusiness;
            this._mapper = mapper;
            this._aws = aws;
        }

        // GET: /Activities
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
        [ProducesResponseType(typeof(IEnumerable<ActivityResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status400BadRequest)]


        [HttpGet]
        [Route("/Activities")]
        public  IActionResult GetAll()
        {
            var activities = _business.Find(c => c.IsDeleted == false);
            if (activities == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<IEnumerable<ActivityResponse>>(activities));
        }

        // GET: /Activities/5
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
        [ProducesResponseType(typeof(ActivityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]

 
        [HttpGet]
        [Route("/Activities/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _business.GetById(id);

            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ActivityResponse>(model));
        }

        // POST: /Activities
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
        [ProducesResponseType(typeof(ActivityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/Activities")]
        public async Task<IActionResult> Create([FromForm] ActivityRequest model)
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

            var newModel = new Activity
            {
                Name = model.Name,
                Image = url,
                Content = model.Content
            };

            var activity = await _business.Insert(_mapper.Map<Activity>(newModel));
            return Ok(_mapper.Map<ActivityResponse>(activity));
        }

        // GET: /Activities/5
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
        [ProducesResponseType(typeof(ActivityResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("/Activities/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] ActivityRequest model)
        {
            var activity = await _business.GetById(id);

            if (activity == null) {
                return NotFound("Not found");
            }

            var url = AWSMockWithOutCredentials.UploadImage(model.Image);
            //var url = await _aws.UploadImage(model.Image);

            if (url == null)
            {
                return NotFound("File is required, to be uploaded.");
            }

            activity.Name = model.Name;
            activity.Image = url;
            activity.Content = model.Content;

            var activityResponse = await _business.Update(activity);

            return Ok(_mapper.Map<ActivityResponse>(activityResponse));
        }

        // Delete: /Activities/5
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
        [Route("/Activities/{id}")]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            var activity = await _business.GetById(id.Value);

            if (activity == null)
            {
                return NotFound();
            }
            await _business.SoftDelete(activity);
            await _business.Update(activity);
            return Ok("Deleted successfully.");
        }
    }
}
