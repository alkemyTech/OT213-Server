using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OngProject.Core.Models.DTOs.Testimonial
{
    public class TestimonialUpdateDTO
    {
        public int Id {set;get;}

        [Required(ErrorMessage = "Nombre requerida")]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Imagen requerida")]
        [DisplayName("Imagen")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Descripci�n requerida")]
        [DisplayName("Descripci�n")]
        public string Description { get; set; }

        public bool IsDeleted { get; set; }
    }
}