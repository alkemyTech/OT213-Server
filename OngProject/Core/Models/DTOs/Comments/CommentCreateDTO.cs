using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs
{
    public class CommentCreateDTO
    {        
        [Required(ErrorMessage = "Name is required")]
        public string Name { set; get; }

        [Required(ErrorMessage = "NewId is required")]
        public int NewId { set; get; }

        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
    }
}

