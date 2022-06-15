using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Auth.Interfaces;
using OngProject.Core.Business.Mail.Interfaces;
using OngProject.Core.Helper.Interface;
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

        public AuthenticationController(IAuthBusiness authBusiness, 
                                        IMailBusiness mailBusiness, 
                                        IMapper mapper,
                                        ITokenService token)
        {
            this._authBusiness = authBusiness;
            this._mailBusiness = mailBusiness;
            this._mapper = mapper;
            this._tokenService = token;
        }

        [HttpPost]
        [Route("Auth/Register")]
        public async Task<IActionResult> Register([FromForm] UserAuthDTO dto)
        {
            dto.Email = dto.Email.ToLower();
            if(await _authBusiness.ExistsUser(dto.Email))
                return BadRequest("User already exists!");           

            var user = await _authBusiness.Registrar(_mapper.Map<User>(dto), dto.Password);
            var mappedUser = _mapper.Map<UserGetModelDTO>(user);
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

        [HttpPost]
        [Route("Auth/Login")]
        public async Task<IActionResult> Login(UserAuthDTO dto)
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

    }

}

