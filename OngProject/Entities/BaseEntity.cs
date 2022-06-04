using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OngProject.Entities
{
    /*
     * timestamps y softDeletes
     * Me parece que esta lo pide en todos los modelos, si les parece se podemos usar así
     * public class <nombremodelo> : BaseModel
     * 
     */
    public class BaseEntity
    {
        [Key]
        [Required]
        [Column("id")]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

        [DefaultValue(false)]
        public bool IsDeleted { set; get; }
    }
}
