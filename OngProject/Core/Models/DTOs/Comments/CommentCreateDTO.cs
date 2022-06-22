using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Comments
{
    public class CommentCreateDTO
    {        
        [Required(ErrorMessage = "Name is required")]
        public string body { set; get; }

        [Required(ErrorMessage = "NewId is required")]
        public int newId { set; get; }

        [Required(ErrorMessage = "UserId is required")]
        public int userId { get; set; }
    }
}

