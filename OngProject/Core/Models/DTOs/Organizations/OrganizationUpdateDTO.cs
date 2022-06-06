using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Core.Models.DTOs.Organizations
{
    public class OrganizationUpdateDTO
    {
        [Required]
        [Column("id")]
        public int Id { set; get; }

        [Required(ErrorMessage = "Nombre requerido")]
        [Column("name")]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Imagen requerida")]
        [Column("image")]
        [DisplayName("Imagen")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Correo electrónico requerido")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [DisplayName("Correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mensaje de bienvenida requerido")]
        [Column("WelcomeText", TypeName = "TEXT")]
        [DisplayName("Mensaje de bienvenida")]
        public string Welcome { get; set; }
    }
}