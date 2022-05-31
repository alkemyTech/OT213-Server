using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Activities : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Image { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
