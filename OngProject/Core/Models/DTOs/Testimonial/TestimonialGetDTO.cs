using System;

namespace OngProject.Core.Models.DTOs.Testimonial
{
    public class TestimonialGetDTO
    {
        public int Id {set;get;}
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}