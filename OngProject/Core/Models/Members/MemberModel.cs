using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models
{
    public class MemberModel
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
