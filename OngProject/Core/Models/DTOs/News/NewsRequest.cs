using Microsoft.AspNetCore.Http;

namespace OngProject.Core.Models.DTOs.News
{
    public class NewsRequest
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public int? CategoryId { get; set; }
    }
}