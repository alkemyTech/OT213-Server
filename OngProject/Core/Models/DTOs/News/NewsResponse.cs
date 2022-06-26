﻿using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.News
{
    public class NewsResponse
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }
        public string Image { get; set; }

        [Required]
        public int? CategoryId { get; set; }
    }
}