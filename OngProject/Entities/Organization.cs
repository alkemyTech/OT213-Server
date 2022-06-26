using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    public class Organization : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        public string Address { get; set; }
        public int Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Welcome { get; set; }

        public string AboutUs { get; set; }

        public string FacebookUrl {set;get;}
        public string InstagramUrl {set;get;}
        public string LinkedInUrl {set;get;}

        // News Navigation property.
        public List<Slide> Slides { set; get; }
    }
}
