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
    
    public partial class RECETA
    {
        public RECETA()
        {
            this.ETIQUETA = new HashSet<ETIQUETA>();
            this.PASO = new HashSet<PASO>();
        }
    
        [Key]
        public int IDRECETA { get; set; }
        public string NOMBRE { get; set; }
        public Nullable<int> PORCIONES { get; set; }
        public string COMPLEJIDAD { get; set; }
        public System.TimeSpan TIEMPO { get; set; }
        public string PAIS { get; set; }
        public string FOTO { get; set; }
        public string TIPOPREPARACION { get; set; }
    
        public virtual ICollection<ETIQUETA> ETIQUETA { get; set; }
        public virtual ICollection<PASO> PASO { get; set; }
    }
}