using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Auth.Interfaces;
using OngProject.Core.Models.DTOs.Users.Auth;
using OngProject.Entities;

namespace OngProject.Controllers
{
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;
        private readonly IMapper _mapper;

        public AuthenticationController(IAuthBusiness authBusiness, IMapper mapper)
        {
            this._authBusiness = authBusiness;
            this._mapper = mapper;
        }

        [HttpPost]
        [Route("Auth/Register")]
        public async Task<IActionResult> Register(UserRegisterModelDTO dto)
        {
            // validations
            dto.Email = dto.Email.ToLower();
            if(await _authBusiness.ExistsUser(dto.Email))
            {
                return StatusCode(StatusCodes.Status400BadRequest, new
                {
                    Status = "Error",
                    Message = "User already exists!"
                });
            }

            // request
            await _authBusiness.Registrar(_mapper.Map<User>(dto), dto.Password);

            return Ok(new 
            {
                Status = "Success",
                Message = "User creation successfully!"
            }); 
        }

    }

}

