//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Recetas_1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class PASO
    {
        public PASO()
        {
            this.INGREDIENTE = new HashSet<INGREDIENTE>();
        }
    
        [Key]
        public int IDPASO { get; set; }
        public int IDRECETA { get; set; }
        public int NUMEROPASO { get; set; }
        public string DESCRIPCION { get; set; }
        public string FOTO { get; set; }
    
        public virtual ICollection<INGREDIENTE> INGREDIENTE { get; set; }
        public virtual RECETA RECETA { get; set; }
    }
}
