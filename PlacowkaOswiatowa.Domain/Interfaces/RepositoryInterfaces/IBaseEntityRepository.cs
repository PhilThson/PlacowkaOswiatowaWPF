using PlacowkaOswiatowa.Domain.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces
{
    public interface IBaseEntityRepository<TEntity, TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id, bool tracked = false);
        TEntity Get(Expression<Func<TEntity, bool>> filter,
            string? includeProperties = null, bool tracked = false);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter,
            string? includeProperties = null, bool tracked = false);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null,
            string? includeProperties = null);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
            string? includeProperties = null);
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task RemoveAsync(TKey id);
        Task RemoveRangeAsync(IEnumerable<TEntity> entity);
        Task<bool> HasAnyAsync();
        Task<bool> ExistsById(TKey id);
        Task<bool> Exists(TEntity entity);
    }
}
