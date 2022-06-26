using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Categories
{
    public class CategoryRequest
    {
        [Required]
        public string Name { set; get; }
        [Required]
        public string Description { set; get; }
        [Required]
        public IFormFile Image { get; set; }
    }
}

