using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class New : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Image { get; set; }      

        // FK_Categories id
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
