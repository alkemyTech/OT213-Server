namespace OngProject.Core.Models.DTOs.Users.Auth
{
    public class UserGetModelDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Photo { get; set; } 
        public int RoleId { get; set; }
    }

}

