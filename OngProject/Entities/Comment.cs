using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Comment : BaseEntity
    {      
        // Fk_users id
        [Required]
        public int? UserId {set;get;}
        public User User { get; set; }

        [Required]
        public string Body {set;get;}

        // Fk_news id
        [Required]
        public int? NewId { get; set; }
        public New New { get; set; }
    }

}

