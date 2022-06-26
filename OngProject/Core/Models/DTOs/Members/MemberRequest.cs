using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Members
{
    public class MemberRequest
    {
        [Required]
        public string Name { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string LinkedInUrl { get; set; }

        [Required]
        public IFormFile ImageUrl { get; set; }
        public string Description { get; set; }
    }
}