using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.News
{
    public class NewsUpdateDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "CategoryID is required")]
        public int CategoryID { get; set; } 

    }
}
