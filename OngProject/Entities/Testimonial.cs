using System;

namespace OngProject.Entities
{
    public class Testimonial : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedAt { get; set; }
        public new bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
