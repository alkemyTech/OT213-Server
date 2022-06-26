using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Users
{
    public class UserRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Photo { get; set; }
        public int RoleId { get; set; }
    }
}