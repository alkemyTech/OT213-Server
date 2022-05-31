using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Activities
{
    public class ActivityCreateDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Image { get; set; }        
    }
}
