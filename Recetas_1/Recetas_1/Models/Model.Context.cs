﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ETIQUETA> ETIQUETA { get; set; }
        public DbSet<INGREDIENTE> INGREDIENTE { get; set; }
        public DbSet<PASO> PASO { get; set; }
        public DbSet<RECETA> RECETA { get; set; }
        public DbSet<ROLINTERNACIONAL> ROLINTERNACIONAL { get; set; }
        public DbSet<ROLNACIONAL> ROLNACIONAL { get; set; }
    }
}
