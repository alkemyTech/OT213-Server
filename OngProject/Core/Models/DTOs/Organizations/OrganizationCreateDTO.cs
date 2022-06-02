using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Core.Models.DTOs.Organizations
{
    public class OrganizationCreateDTO
    {
        [Required(ErrorMessage = "Nombre requerido")]
        [Column("name")]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Imagen requerida")]
        [Column("image")]
        [DisplayName("Imagen")]
        public string Image { get; set; }

        [Column("address")]
        [DisplayName("Dirección")]
        public string Address { get; set; }

        [Column("phone")]
        [DisplayName("Número de teléfono")]
        public int Phone { get; set; }

        [Required(ErrorMessage = "Correo electrónico requerido")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [DisplayName("Correo electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mensaje de bienvenida requerido")]
        [Column("WelcomeText", TypeName = "TEXT")]
        [DisplayName("Mensaje de bienvenida")]
        public string Welcome { get; set; }

        [Column("AboutUsText", TypeName = "TEXT")]
        [DisplayName("Acerca de nosotros")]
        public string AboutUs { get; set; }
    }
}
