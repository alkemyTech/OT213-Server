using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Activities
{
    public class ActivitiesGetDTO
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
    }
}