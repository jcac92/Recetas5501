using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Recetas_1.Models
{ 
    public class ETIQUETARepository : IETIQUETARepository
    {
        Entities context = new Entities();

        public IQueryable<ETIQUETA> All
        {
            get { return context.ETIQUETA; }
        }

        public IQueryable<ETIQUETA> AllIncluding(params Expression<Func<ETIQUETA, object>>[] includeProperties)
        {
            IQueryable<ETIQUETA> query = context.ETIQUETA;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ETIQUETA Find(int id)
        {
            return context.ETIQUETA.Find(id);
        }

        public void InsertOrUpdate(ETIQUETA etiqueta)
        {
            if (etiqueta.IDETIQUETA == default(int)) {
                // New entity
                context.ETIQUETA.Add(etiqueta);
            } else {
                // Existing entity
                context.Entry(etiqueta).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var etiqueta = context.ETIQUETA.Find(id);
            context.ETIQUETA.Remove(etiqueta);
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

    public interface IETIQUETARepository : IDisposable
    {
        IQueryable<ETIQUETA> All { get; }
        IQueryable<ETIQUETA> AllIncluding(params Expression<Func<ETIQUETA, object>>[] includeProperties);
        ETIQUETA Find(int id);
        void InsertOrUpdate(ETIQUETA etiqueta);
        void Delete(int id);
        void Save();
    }
}