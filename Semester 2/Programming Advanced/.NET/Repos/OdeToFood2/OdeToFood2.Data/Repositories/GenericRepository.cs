using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OdeToFood2.Data.IRepositories;

namespace OdeToFood2.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly OdeToFoodContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(OdeToFoodContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        #region Async versions
        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = await _dbSet.AddAsync(entity);
            await SaveAsync();
            return addedEntity.Entity;
        }

        public async Task UpdateAsync(int id, TEntity entity)
        {
            var originalEntity = await _dbSet.FindAsync(id);
            _context.Entry(originalEntity).CurrentValues.SetValues(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }
        private async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

    }
}
