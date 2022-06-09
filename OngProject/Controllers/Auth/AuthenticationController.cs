using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Auth.Interfaces;
using OngProject.Core.Business.Mail.Interfaces;
using OngProject.Core.Models.DTOs.Users.Auth;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;
        private readonly IMailBusiness _mailBusiness;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthBusiness authBusiness, IMailBusiness mailBusiness, IMapper mapper)
        {
            this._authBusiness = authBusiness;
            this._mailBusiness = mailBusiness;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("Auth/Register")]
        public async Task<IActionResult> Register(UserRegisterModelDTO dto)
        {
            // validations
            dto.Email = dto.Email.ToLower();
            if (await _authBusiness.ExistsUser(dto.Email))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "User already exists!"
                });
            }

            // request
            await _authBusiness.Registrar(_mapper.Map<User>(dto), dto.Password);
            await _mailBusiness.SendEmailAsync(dto.Email);

            return Ok(new
            {
                Status = "Success",
                Message = "User creation successfully!"
            });
        }

        [HttpPost]
        [Route("auth/login")]
        public async Task<IActionResult> Login(UserLoginDTO user)
        {
            if(!await _authBusiness.ExistsUser(user.Email))
            {
                return BadRequest("User doesn't exist");
            }
            if(_authBusiness.Login(credentials user.Password, ))
            return Ok(user);
        }

    }

}

