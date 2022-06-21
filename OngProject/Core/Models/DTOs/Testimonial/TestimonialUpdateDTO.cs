using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Testimonial
{
    public class TestimonialUpdateDTO
    {
        public int Id {set;get;}

        [Required(ErrorMessage = "Name required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Image required")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Description required")]
        public string Description { get; set; }

    }
}