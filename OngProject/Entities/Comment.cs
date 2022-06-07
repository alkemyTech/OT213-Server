using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Comment : BaseEntity
    {      
        // Fk_users id
        [Required]
        public int User_Id {set;get;}
        public User User { get; set; }

        [Required]
        public string Body {set;get;}

        // Fk_news id
        [Required]
        public int? New_Id { get; set; }
        public New New { get; set; }
    }

}

