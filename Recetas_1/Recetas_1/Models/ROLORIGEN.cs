using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recetas_1.Models
{
    public abstract class ROLORIGEN
    {
        public int IDRECETA { get; set; }

        public int getIdReceta()
        {
            return this.IDRECETA;
        }
    }
}