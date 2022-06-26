using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace OngProject.Core.Models.DTOs.Activities
{
    public class ActivityRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
