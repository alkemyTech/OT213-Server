namespace OngProject.Core.Models.DTOs.Comments
{
    public class CommentResponse
    {
        public int Id { get; set; }

        public int? UserId { set; get; }
        public int? NewsId { get; set; }
        public string Body { get; set; }
    }
}

