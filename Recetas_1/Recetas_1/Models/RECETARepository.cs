using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Recetas_1.Models
{ 
    public class RECETARepository : IRECETARepository
    {
        Entities context = new Entities();

        public IQueryable<RECETA> All
        {
            get { return context.RECETA; }
        }

        public IQueryable<RECETA> AllIncluding(params Expression<Func<RECETA, object>>[] includeProperties)
        {
            IQueryable<RECETA> query = context.RECETA;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public RECETA Find(int id)
        {
            return context.RECETA.Find(id);
        }

        public void InsertOrUpdate(RECETA receta)
        {
            if (receta.IDRECETA == default(int)) {
                // New entity
                context.RECETA.Add(receta);
            } else {
                // Existing entity
                context.Entry(receta).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var receta = context.RECETA.Find(id);
            context.RECETA.Remove(receta);
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

    public interface IRECETARepository : IDisposable
    {
        IQueryable<RECETA> All { get; }
        IQueryable<RECETA> AllIncluding(params Expression<Func<RECETA, object>>[] includeProperties);
        RECETA Find(int id);
        void InsertOrUpdate(RECETA receta);
        void Delete(int id);
        void Save();
    }
}