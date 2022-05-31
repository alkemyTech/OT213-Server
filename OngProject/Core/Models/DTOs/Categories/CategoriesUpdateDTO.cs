using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models
{
    public class CategoriesUpdateDTO
    {
        public int Id { set; get; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { set; get; }
        public string Description { set; get; }

        [Required(ErrorMessage = "Image is required")]
        public string Image { get; set; }
        public bool softDelete { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
