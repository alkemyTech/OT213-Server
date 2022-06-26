using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Comments
{
    public class CommentRequest
    {
        public int? UserId { set; get; }
        public int? NewsId { get; set; }
        [Required]
        public string Body { set; get; }
    }
}

