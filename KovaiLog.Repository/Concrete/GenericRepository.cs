using KovaiLog.Entities.Models;
using KovaiLog.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace KovaiLog.Repository.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private KovaiLogDBContext context;
        private DbSet<TEntity> dbSet;
        public GenericRepository(KovaiLogDBContext dbContext)
        {
            context = dbContext;
            dbSet = context.Set<TEntity>();
        }

        public GenericRepository()
        {
            this.dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }
        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }
        public TEntity Insert(TEntity obj)
        {
            dbSet.Add(obj);
            Save();
            return obj;
        }
        public void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }
        public void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            Save();
        }
        public TEntity Update(TEntity obj, int Id)
        {
            var original = dbSet.Find(Id);
            context.Entry<TEntity>(original).CurrentValues.SetValues(obj);            
            Save();
            return obj;
        }
        public void Save()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        //System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                    context = null;
                }
            }
        }
    }
}