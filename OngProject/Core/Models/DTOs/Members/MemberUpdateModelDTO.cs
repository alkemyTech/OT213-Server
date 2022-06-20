using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Members
{
    public class MemberUpdateModelDTO
    {
        public int Id {set;get;}

        [Required]
        public string name {set;get;}
        public string facebookUrl {set;get;}
        public string instagramUrl {set;get;}
        public string linkedInUrl {set;get;}

        [Required]
        public string imageUrl {set;get;}
        public string description {set;get;}
    }
}