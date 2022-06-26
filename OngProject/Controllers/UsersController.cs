using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using System.Threading.Tasks;
using AutoMapper;
using OngProject.Core.Models.DTOs.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersBusiness _business;
        private readonly IMapper _mapper;
        public UsersController(IUsersBusiness usersBusiness, IMapper mapper)
        {
            this._business = usersBusiness;
            this._mapper = mapper;
        }

        // GET: /Users
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
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status400BadRequest)]

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("/Users")]
        public IActionResult GetAll()
        {
            var users = _business.Find(c => c.IsDeleted == false);
            if (users == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<IEnumerable<UserResponse>>(users));
        }

        // GET: /Users/5
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
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]

        [HttpGet]
        [Route("/Users/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = await _business.GetById(id);

            if (model == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<UserResponse>(model));
        }

        // POST: /Users
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
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status400BadRequest)]

        [HttpPost]
        [Route("/Users")]
        public async Task<IActionResult> Create([FromBody] UserRequest model)
        {
            if (model is null)
            {
                return BadRequest();
            }

            var user = await _business.Insert(_mapper.Map<User>(model));
            return Ok(_mapper.Map<UserResponse>(user));
        }

        // GET: /Users/5
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
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status404NotFound)]

        [HttpPut]
        [Route("/Users/{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UserRequest model)
        {
            var user = await _business.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(model, user);
            var userResponse = await _business.Update(user);

            return Ok(_mapper.Map<UserResponse>(userResponse));
        }

        // Delete: /Users/5
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
        [Route("/Users/{id}")]
        public async Task<IActionResult> SoftDelete(int? id)
        {
            var user = await _business.GetById(id.Value);

            if (user == null)
            {
                return NotFound();
            }
            await _business.SoftDelete(user);
            await _business.Update(user);
            return Ok("Deleted successfully.");
        }
    }
}