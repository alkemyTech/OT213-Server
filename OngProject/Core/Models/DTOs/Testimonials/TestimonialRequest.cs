using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Testimonials
{
    public class TestimonialRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Description { get; set; }
    }
}