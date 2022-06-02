using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; } 
        public string Description { get; set; }  
        public string Image { get; set; }

        // News Navigation property.
        public List<News> News { set; get; }
    }
}
