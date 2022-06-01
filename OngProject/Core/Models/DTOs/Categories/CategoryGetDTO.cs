using System;

namespace OngProject.Core.Models.DTOs.Categories
{
    public class CategoryGetDTO
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}

