using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Organizations
{
    public class OrganizationCreateDTO
    {
        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Image required")]
        public string Image { get; set; }

        public string Address { get; set; }
        public int Phone { get; set; }

        [Required(ErrorMessage = "Email required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Welcome message required")]
        public string Welcome { get; set; }
        public string AboutUs { get; set; }
    }
}
