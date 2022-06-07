using System;


namespace OngProject.Core.Models.DTOs
{
    public class SlideDetailsDTO
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }

        public int Order { get; set; }

        public int? OrganizationId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

