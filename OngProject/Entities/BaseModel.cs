using System;

namespace OngProject.Entities
{
    /*
     * timestamps y softDeletes
     * Me parece que esta lo pide en todos los modelos, si les parece se podemos usar así
     * public class <nombremodelo> : BaseModel
     * 
     */
    public class BaseModel
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { set; get; }
    }
}