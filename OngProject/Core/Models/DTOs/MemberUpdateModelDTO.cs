using System;

namespace OngProject.Core.Models.DTOs
{
    public class MemberUpdateModelDTO
    {
        public int membersID {set;get;}
        public string name {set;get;}
        public string facebookUrl {set;get;}
        public string instagramUrl {set;get;}
        public string linkedInUrl {set;get;}
        public string imageUrl {set;get;}
        public string description {set;get;}
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
        public bool isDeleted {set;get;} = false;    

    }
}