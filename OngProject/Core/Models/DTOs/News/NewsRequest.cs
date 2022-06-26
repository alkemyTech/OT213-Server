namespace OngProject.Core.Models.DTOs.News
{
    public class NewsRequest
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public int? CategoryId { get; set; }
    }
}