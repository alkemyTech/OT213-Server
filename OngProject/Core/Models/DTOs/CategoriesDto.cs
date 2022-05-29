using System;

namespace OngProject.Core.Models
{
    public class CategoriesDTO
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Image { get; set; }
        public bool softDelete { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
