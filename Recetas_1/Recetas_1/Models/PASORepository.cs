using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Recetas_1.Models
{ 
    public class PASORepository : IPASORepository
    {
        Entities context = new Entities();

        public IQueryable<PASO> All
        {
            get { return context.PASO; }
        }

        public IQueryable<PASO> AllIncluding(params Expression<Func<PASO, object>>[] includeProperties)
        {
            IQueryable<PASO> query = context.PASO;
            foreach (var includeProperty in includeProperties) {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public PASO Find(int id)
        {
            return context.PASO.Find(id);
        }

        public void InsertOrUpdate(PASO paso)
        {
            if (paso.IDPASO == default(int)) {
                // New entity
                context.PASO.Add(paso);
            } else {
                // Existing entity
                context.Entry(paso).State = System.Data.Entity.EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var paso = context.PASO.Find(id);
            context.PASO.Remove(paso);
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

    public interface IPASORepository : IDisposable
    {
        IQueryable<PASO> All { get; }
        IQueryable<PASO> AllIncluding(params Expression<Func<PASO, object>>[] includeProperties);
        PASO Find(int id);
        void InsertOrUpdate(PASO paso);
        void Delete(int id);
        void Save();
    }
}