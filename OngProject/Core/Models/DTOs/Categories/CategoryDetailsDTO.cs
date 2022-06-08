using System;

namespace OngProject.Core.Models.DTOs.Categories
{
    public class CategoryDetailsDTO
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; }

    }

}

