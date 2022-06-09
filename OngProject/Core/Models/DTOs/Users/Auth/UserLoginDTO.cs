using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Users.Auth
{
    public class UserLoginDTO
    {
        [Required]
        public string FirstName { get; set; }
     //   [Required]
	    //[EmailAddress]
     //   public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }

}

