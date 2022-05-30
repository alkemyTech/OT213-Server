using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Role : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        //Roles navigation property.
        public List<User> Users { set; get; }
    }
}
