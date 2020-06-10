using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Stage_API.Data.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id, params Expression<Func<TEntity, object>>[] includes);
        TEntity GetById(int id);
        TEntity GetProfile(int id);
        IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> statement, params Expression<Func<TEntity, object>>[] includes);
        void Add(TEntity entity);
        void Remove(TEntity entity);
        bool Update(int id, TEntity entity);
        void Save();
    }
}