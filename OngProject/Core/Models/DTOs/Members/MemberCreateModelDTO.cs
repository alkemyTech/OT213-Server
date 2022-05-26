using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs.Members
{
    public class MemberCreateModelDTO
    {
        public string name {set;get;}
        public string facebookUrl {set;get;}
        public string instagramUrl {set;get;}
        public string linkedInUrl {set;get;}
        public string imageUrl {set;get;}
        public string description {set;get;}
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
    }
}
