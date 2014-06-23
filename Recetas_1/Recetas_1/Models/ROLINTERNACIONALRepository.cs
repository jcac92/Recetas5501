using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Recetas_1.Models
{ 
    public class ROLINTERNACIONALRepository : IROLINTERNACIONALRepository
    {
        Entities context = new Entities();

        public IQueryable<ROLINTERNACIONAL> All
        {
            get { return context.ROLINTERNACIONAL; }
        }

        public IQueryable<ROLINTERNACIONAL> AllIncluding(params Expression<Func<ROLINTERNACIONAL, object>>[] includeProperties)
        {
            IQueryable<ROLINTERNACIONAL> query = context.ROLINTERNACIONAL;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public ROLINTERNACIONAL Find(int id)
        {
            return context.ROLINTERNACIONAL.Find(id);
        }

        public void InsertOrUpdate(ROLINTERNACIONAL rolinternacional)
        {
            if (rolinternacional.IDROLINTERNACIONAL == default(int)) {
                // New entity
                context.ROLINTERNACIONAL.Add(rolinternacional);
            } else {
                // Existing entity
                context.Entry(rolinternacional).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var rolinternacional = context.ROLINTERNACIONAL.Find(id);
            context.ROLINTERNACIONAL.Remove(rolinternacional);
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

    public interface IROLINTERNACIONALRepository : IDisposable
    {
        IQueryable<ROLINTERNACIONAL> All { get; }
        IQueryable<ROLINTERNACIONAL> AllIncluding(params Expression<Func<ROLINTERNACIONAL, object>>[] includeProperties);
        ROLINTERNACIONAL Find(int id);
        void InsertOrUpdate(ROLINTERNACIONAL rolinternacional);
        void Delete(int id);
        void Save();
    }
}