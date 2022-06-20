using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using OngProject.Entities;

namespace OngProject.Core.Models.DTOs.News
{
    public class NewsDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }
        public string Image { get; set; }

        //[JsonIgnore]
        [ForeignKey("CategoryID ")]
        public int CategoryID { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

    }
}
