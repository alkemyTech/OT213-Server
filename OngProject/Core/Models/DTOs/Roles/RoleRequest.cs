using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Roles
{
    public class RoleRequest
    {
        [Required]
        public string Name { set; get; }

        [Required]
        public string Description { set; get; }
    }
}