using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Slides
{
    public class SlideRequest
    {
        [Required]
        public string Name { set; get; }
        public string Text { set; get; }

        [Required]
        public string ImageUrl { get; set; }

        public int Order { get; set; }
        public int? OrganizationId { get; set; }
    }
}