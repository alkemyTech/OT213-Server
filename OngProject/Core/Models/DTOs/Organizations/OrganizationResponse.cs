namespace OngProject.Core.Models.DTOs.Organizations
{
    public class OrganizationResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
        public string Welcome { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string AboutUs { get; set; }
        public string FacebookUrl { set; get; }
        public string InstagramUrl { set; get; }
        public string LinkedInUrl { set; get; }
    }
}