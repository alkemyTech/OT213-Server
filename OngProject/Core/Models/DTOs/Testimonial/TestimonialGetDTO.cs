using System;

namespace OngProject.Core.Models.DTOs.Testimonial
{
    public class TestimonialGetDTO
    {
        public int Id {set;get;}
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }

    }

}

