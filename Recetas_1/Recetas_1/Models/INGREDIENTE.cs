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
    
    public partial class INGREDIENTE
    {
        [Key]
        public int IDINGREDIENTE { get; set; }
        public int IDPASO { get; set; }
        public string NOMBRE { get; set; }
        public string CANTIDAD { get; set; }
    
        public virtual PASO PASO { get; set; }

        public int getIdIngrediente()
        {
            return this.IDINGREDIENTE;
        }

        public int getIdPaso()
        {
            return this.IDPASO;
        }

        public string getNombre()
        {
            return this.NOMBRE;
        }

        public string getCantidad()
        {
            return this.CANTIDAD;
        }
    }
}
