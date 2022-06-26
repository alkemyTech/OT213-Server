using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class News : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Image { get; set; }      

        // FK_Categories id
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        // News Navigation property.
        public List<Comment> Comments { set; get; }
    }
}
