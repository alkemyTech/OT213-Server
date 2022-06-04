using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Contact : BaseEntity
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name {set;get;}        
        public string Phone {set;get;}
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }
        public string Message {set;get;}
        public string deletedAt {set;get;}
    }

}

