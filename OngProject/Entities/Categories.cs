using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Categories : BaseEntity
    {

        [Required]
        public string Name { get; set; }
 
        public string Description { get; set; }
  
        public string Image { get; set; }


    }
}
