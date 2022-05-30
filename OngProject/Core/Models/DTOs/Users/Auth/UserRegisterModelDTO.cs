using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Users.Auth
{
    public class UserRegisterModelDTO
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
        public string Photo { get; set; } 
    }

}

