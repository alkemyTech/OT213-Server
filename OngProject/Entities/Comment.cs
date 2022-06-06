using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Comment
    {        
        [Key]
        [Required]
        public int User_Id {set;get;}
        [Required]
        public string Body {set;get;}

        // Fk_news id
        public int? NewId { get; set; }
        public New New { get; set; }
    }

}

