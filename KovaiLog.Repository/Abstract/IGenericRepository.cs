using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace KovaiLog.Repository.Abstract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(object Id);
        TEntity Insert(TEntity obj);
        void Delete(object Id);
        TEntity Update(TEntity obj, int Id);
        void Save();
    }
}