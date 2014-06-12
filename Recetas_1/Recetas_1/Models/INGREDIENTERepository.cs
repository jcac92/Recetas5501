using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Recetas_1.Models
{ 
    public class INGREDIENTERepository : IINGREDIENTERepository
    {
        Entities context = new Entities();

        public IQueryable<INGREDIENTE> All
        {
            get { return context.INGREDIENTE; }
        }

        public IQueryable<INGREDIENTE> AllIncluding(params Expression<Func<INGREDIENTE, object>>[] includeProperties)
        {
            IQueryable<INGREDIENTE> query = context.INGREDIENTE;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public INGREDIENTE Find(int id)
        {
            return context.INGREDIENTE.Find(id);
        }

        public void InsertOrUpdate(INGREDIENTE ingrediente)
        {
            if (ingrediente.IDINGREDIENTE == default(int)) {
                // New entity
                context.INGREDIENTE.Add(ingrediente);
            } else {
                // Existing entity
                context.Entry(ingrediente).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var ingrediente = context.INGREDIENTE.Find(id);
            context.INGREDIENTE.Remove(ingrediente);
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose() 
        {
            context.Dispose();
        }
    }

    public interface IINGREDIENTERepository : IDisposable
    {
        IQueryable<INGREDIENTE> All { get; }
        IQueryable<INGREDIENTE> AllIncluding(params Expression<Func<INGREDIENTE, object>>[] includeProperties);
        INGREDIENTE Find(int id);
        void InsertOrUpdate(INGREDIENTE ingrediente);
        void Delete(int id);
        void Save();
    }
}