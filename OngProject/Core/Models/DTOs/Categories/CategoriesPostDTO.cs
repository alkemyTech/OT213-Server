using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models
{
    public class CategoriesPostDTO
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { set; get; }
        public string Description { set; get; }
        public string Image { get; set; }
        public bool softDelete { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
