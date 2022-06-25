using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Users
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; }
        public int RoleId { get; set; }
    }
}
