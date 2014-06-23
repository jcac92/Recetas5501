using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Recetas_1.Models
{ 
    public class ROLNACIONALRepository : IROLNACIONALRepository
    {
        Entities context = new Entities();

        public IQueryable<ROLNACIONAL> All
        {
            get { return context.ROLNACIONAL; }
        }

        public IQueryable<ROLNACIONAL> AllIncluding(params Expression<Func<ROLNACIONAL, object>>[] includeProperties)
        {
            IQueryable<ROLNACIONAL> query = context.ROLNACIONAL;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ROLNACIONAL Find(int id)
        {
            return context.ROLNACIONAL.Find(id);
        }

        public void InsertOrUpdate(ROLNACIONAL rolnacional)
        {
            if (rolnacional.IDROLNACIONAL == default(int)) {
                // New entity
                context.ROLNACIONAL.Add(rolnacional);
            } else {
                // Existing entity
                context.Entry(rolnacional).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var rolnacional = context.ROLNACIONAL.Find(id);
            context.ROLNACIONAL.Remove(rolnacional);
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

    public interface IROLNACIONALRepository : IDisposable
    {
        IQueryable<ROLNACIONAL> All { get; }
        IQueryable<ROLNACIONAL> AllIncluding(params Expression<Func<ROLNACIONAL, object>>[] includeProperties);
        ROLNACIONAL Find(int id);
        void InsertOrUpdate(ROLNACIONAL rolnacional);
        void Delete(int id);
        void Save();
    }
}