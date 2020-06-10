using Microsoft.EntityFrameworkCore;
using Stage_API.Data.IRepositories;
using Stage_API.Domain;
using Stage_API.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Stage_API.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly StageContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(StageContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            var entities = _dbSet.ToList();
            return GetIncludes(entities, includes);
        }


        public virtual IEnumerable<TEntity> GetAll()
        {
            var entities = _dbSet.ToList();
            return entities;
        }

        public virtual TEntity GetById(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var entity = _dbSet.Find(id);
            return entity == null ? null : GetIncludes(entity, includes);
        }

        public virtual TEntity GetById(int id)
        {
            var entity = _dbSet.Find(id);
            return entity;
        }

        public TEntity GetProfile(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null) return null;
            if (entity.GetType() != typeof(Bedrijf)) return entity;
            _context.Entry((Bedrijf)(User)(object)entity).Reference(b => b.Contactpersoon).Load();
            _context.Entry((Bedrijf)(User)(object)entity).Reference(b => b.Bedrijfspromotor).Load();
            return entity;
        }


        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> statement, params Expression<Func<TEntity, object>>[] includes)
        {
            var entities = _dbSet.Where(statement).ToList();

            return includes.Length == 0 ? entities : GetIncludes(entities, includes);
        }

        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            Save();
        }

        public void Remove(TEntity entity)
        {
            if (entity is Bedrijf bedrijf)
            {
                _context.Entry(bedrijf).Collection(b => b.Stagevoorstellen).Load();
                foreach (var stagevoorstel in bedrijf.Stagevoorstellen)
                {
                    _context.Stagevoorstellen.Remove(stagevoorstel);
                }
            }
            _dbSet.Remove(entity);
            Save();
        }

        public virtual bool Update(int id, TEntity entity)
        {
            var originalEntity = _dbSet.Find(id);
            if (originalEntity == null)
            {
                return false;
            }

            _context.Entry(originalEntity).CurrentValues.SetValues(entity);
            Save();

            return true;
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        #region Include handling

        //for a collection
        private IEnumerable<TEntity> GetIncludes(IList<TEntity> entities, params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (var entity in entities)
            {
                foreach (var include in includes)
                {
                    GetInclude(entity, include);
                }
            }
            return entities;
        }

        //for a single entity
        private TEntity GetIncludes(TEntity entity, params Expression<Func<TEntity, object>>[] includes)
        {
            foreach (var include in includes)
            {
                GetInclude(entity, include);
            }

            return entity;
        }

        private void GetInclude(TEntity entity, Expression<Func<TEntity, object>> include)
        {
            //depending on if the expression returns a single object or an enumerable, it has to be handled differently.
            //As the include is a Linq expression, the only way to find out is to check on if it's plural or not.
            var expressionString = include.ToString();
            var plural = expressionString.EndsWith("en") || expressionString.EndsWith("s");
            if (plural)
            {
                _context.Entry(entity).Collection(expressionString.Split('.')[1]).Load();
            }
            else
            {
                _context.Entry(entity).Reference(include).Load();
            }
        }
        #endregion
    }
}
