using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Auth.Interfaces;
using OngProject.Core.Business.Mail.Interfaces;
using OngProject.Core.Helper.Interface;
using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs.Users.Auth;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;
        private readonly IMailBusiness _mailBusiness;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;
        private readonly IUsersBusiness _userBusiness;

        public AuthenticationController(IAuthBusiness authBusiness, 
                                        IMailBusiness mailBusiness, 
                                        IMapper mapper,
                                        ITokenService token,
                                        IHttpContextAccessor accessor,
                                        IUsersBusiness userBusiness)
        {
            this._authBusiness = authBusiness;
            this._mailBusiness = mailBusiness;
            this._mapper = mapper;
            this._tokenService = token;
            this._accessor = accessor;
            this._userBusiness = userBusiness;
        }

        // POST: /Auth/Register
        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <remarks>
        /// Registra un nuevo usuario.
        /// </remarks>
        /// <param name="dto">Objeto a crear a la BD.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">Created. Objeto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]

        [HttpPost]
        [Route("Auth/Register")]
        public async Task<IActionResult> Register([FromForm] UserAuthDTO dto)
        {
            dto.Email = dto.Email.ToLower();
            if(await _authBusiness.ExistsUser(dto.Email))
                return BadRequest("User already exists!");           

            var user = await _authBusiness.Registrar(_mapper.Map<User>(dto), dto.Password);
            var mappedUser = _mapper.Map<UserGetDTO>(user);
            
            await _mailBusiness.SendEmailAsync(dto.Email);

            var validate = await _authBusiness.Login(dto.Email, dto.Password);
            if(validate == null)
                return Unauthorized(); 

            var token = _tokenService.CreateToken(validate); 

            return Ok(new 
            {
                Status = "Success",
                Message = "User registered successfully!",
                User = mappedUser,
                Token = token
            }); 
        }

        // POST: Auth/Login
        /// <summary>
        /// Inicia sesión el usuario.
        /// </summary>
        /// <remarks>
        /// Inicia sesión el usuario.
        /// </remarks>
        /// <param name="dto">Objeto a consultar a la BD.</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el Token JWT de acceso.</response>              
        /// <response code="201">Created. Objeto correctamente creado en la BD.</response>        
        /// <response code="400">BadRequest. No se ha creado el objeto en la BD. Formato del objeto incorrecto.</response>
        [ProducesResponseType(typeof(EmptyResult), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]

        [HttpPost]
        [Route("Auth/Login")]
        public async Task<IActionResult> Login(UserAuthLoginDTO dto)
        {
            var user = await _authBusiness.Login(dto.Email, dto.Password);
            if(user == null)
                return Unauthorized();            
                
            var token = _tokenService.CreateToken(user);  

            return Ok(new 
            {
                Token = token
            });         
        }

        // GET: /Auth/Me
        /// <summary>
        /// Obtiene el usuario logueado por su email.
        /// </summary>
        /// <remarks>
        /// Devuelve el objeto por su email si existe.
        /// </remarks>
        /// <param name="id">Id (int) del objeto.</param>
        /// <response code="200">OK. Devuelve el objeto solicitado.</response>        
        /// <response code="404">NotFound. No se ha encontrado el objeto solicitado.</response>        
        [ProducesResponseType(typeof(IEnumerable<UserGetDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]

        [HttpGet]
        [Route("Auth/Me")]
        public  IActionResult GetMe()
        {            
            var email = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            if(email == null)
                return BadRequest("The claim is null, you have to login first");

            var entity = _userBusiness.Find(u => u.Email == email); 
            var mappedUser = _mapper.Map<IEnumerable<UserGetDTO>>(entity);            

            return Ok(new 
            {
                Entity = mappedUser
            });         
        }

    }

}

