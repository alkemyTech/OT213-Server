using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class SlideCreateDTO
    {        
        [Required(ErrorMessage = "Name is required")]
        public string Name { set; get; }
        public string Text { set; get; }

        [Required(ErrorMessage = "Image is required")]
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        public int? OrganizationId { get; set; }

    }
}

