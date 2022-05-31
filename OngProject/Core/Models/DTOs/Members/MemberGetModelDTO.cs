using System;

namespace OngProject.Core.Models.DTOs.Members
{
    public class MemberGetModelDTO
    {
        public int Id {set;get;}
        public string Name {set;get;}
        public string FacebookUrl {set;get;}
        public string InstagramUrl {set;get;}
        public string LinkedInUrl {set;get;}
        public string ImageUrl {set;get;}
        public string Description {set;get;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}

