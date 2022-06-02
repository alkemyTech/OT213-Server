using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Entities
{
    public class Users
    {
        [Key]
        [Required]
        public int Id { get; set; }
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
        public bool softDelete { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<Role> Roles { set; get; }
    }
}
