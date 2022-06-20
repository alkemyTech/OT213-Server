﻿using System;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Roles
{
    public class RoleModelDto
    {
        public int Id { set; get; }
        
        [Required(ErrorMessage = "Name is required")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { set; get; }
    }
}
