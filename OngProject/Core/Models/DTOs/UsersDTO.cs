using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class UsersDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required"), MinLength(4, ErrorMessage = "Minimum 4 characters required")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
        public bool softDelete { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<Role> Roles { set; get; }
    }
}
