using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Models.DTOs.Users.Auth
{
    public class UserAuthDTO
    {
        [Required(ErrorMessage = "FirstName is required")]
        [RegularExpression("^[a-zA-Z ]*$")]
        [MinLength(5)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [RegularExpression("^[a-zA-Z ]*$")]
        [MinLength(6)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ImgFile is required")]
        public IFormFile ImgFile { get; set; }

    }

}

