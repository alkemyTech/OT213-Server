using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Models.DTOs.Users.Auth
{
    public class UserAuthDTO
    {
        [Required]
	    [MinLength(6)]
        public string FirstName { get; set; }
        [Required]
	    [MinLength(6)]
        public string LastName { get; set; }
        [Required]
	    [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public IFormFile Photo { get; set; } 
    }

}
