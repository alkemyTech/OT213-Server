using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Slides
{
    public class SlideRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { set; get; }
        public string Text { set; get; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile ImageUrl { get; set; }

        public int Order { get; set; }
        [Required(ErrorMessage = "OrganizationId is required")]
        public int? OrganizationId { get; set; }
    }
}