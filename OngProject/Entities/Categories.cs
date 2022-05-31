using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Categories
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
 
        public string Description { get; set; }
  
        public string Image { get; set; }


        public bool softDelete { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
