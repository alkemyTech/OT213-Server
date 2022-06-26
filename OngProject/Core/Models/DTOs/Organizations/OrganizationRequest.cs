using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Organizations
{
    public class OrganizationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Welcome { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string AboutUs { get; set; }
        public string FacebookUrl { set; get; }
        public string InstagramUrl { set; get; }
        public string LinkedInUrl { set; get; }
    }
}