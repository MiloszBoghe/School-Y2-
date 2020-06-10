using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OdeToFood2.Data.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(int id, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}