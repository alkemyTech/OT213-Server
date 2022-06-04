using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Slide : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public int Order { get; set; }

        public int? OrganizationId { get; set; }
        public Organization Organization { get; set;}
    }
}

