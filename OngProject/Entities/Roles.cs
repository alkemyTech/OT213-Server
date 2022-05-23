using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Roles
    {
        /*id: INTEGER NOT NULL AUTO_INCREMENT
        name: VARCHAR NOT NULL
        description: VARCHAR NULLABLE
        timestamps*/
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
