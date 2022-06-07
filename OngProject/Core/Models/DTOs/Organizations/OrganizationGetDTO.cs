namespace OngProject.Core.Models.DTOs.Organizations
{
    public class OrganizationGetDTO
    {
        public string Name { get; set; }      
        public string Image { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string FacebookUrl {set;get;}
        public string InstagramUrl {set;get;}
        public string LinkedInUrl {set;get;}
    }
}
