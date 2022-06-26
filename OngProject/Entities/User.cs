using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]        
        public string Password { get; set; }
        public string Photo { get; set; }
        public byte[] PasswordHash {get;set;}
        public byte[] PasswordSalt {get;set;}

        // FK_Role id
        public int? RoleId { get; set; }
        public Role Role { get; set; }

        // News Navigation property.
        public List<Comment> Comments { set; get; }
    }
}
