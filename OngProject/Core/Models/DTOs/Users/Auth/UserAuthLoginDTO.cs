using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Users.Auth
{
    public class UserAuthLoginDTO
    {
        [Required]
	    [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }

}

