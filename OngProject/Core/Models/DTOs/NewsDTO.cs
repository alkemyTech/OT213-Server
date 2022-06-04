using OngProject.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OngProject.Core.Models.DTOs
{
    public class NewsDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required"), MinLength(4, ErrorMessage = "Minimum 4 characters required")]
        public string Name { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public bool softDelete { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public List<Category> Categories { set; get; }
    }
}
