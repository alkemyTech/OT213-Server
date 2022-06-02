using System;

namespace OngProject.Core.Models.DTOs
{
    public class RoleModelDto
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public DateTime? TimeStamp { get; set; }
    }
}
