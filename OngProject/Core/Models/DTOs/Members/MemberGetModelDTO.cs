using System;

namespace OngProject.Core.Models.DTOs.Members
{
    public class MemberGetModelDTO
    {
        public int MembersID {set;get;}
        public string Name {set;get;}
        public string FacebookUrl {set;get;}
        public string InstagramUrl {set;get;}
        public string LinkedInUrl {set;get;}
        public string ImageUrl {set;get;}
        public string Description {set;get;}
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool isDeleted {set;get;} = false; 
    }

}

