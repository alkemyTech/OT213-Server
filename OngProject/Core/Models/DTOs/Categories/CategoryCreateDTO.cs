using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Categories
{
    public class CategoryCreateDTO
    {        
        [Required(ErrorMessage = "Name is required")]
        public string Name { set; get; }
        public string Description { set; get; }

        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }
    }
}

