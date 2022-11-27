using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PlacowkaOswiatowa.Domain.Interfaces.RepositoryInterfaces;
using PlacowkaOswiatowa.Domain.Models.Base;
using PlacowkaOswiatowa.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PlacowkaOswiatowa.Infrastructure.Repository
{
    public class BaseEntityRepository<TEntity, TKey> : IBaseEntityRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        private readonly AplikacjaDbContext _db;
        internal DbSet<TEntity> _dbSet;

        public BaseEntityRepository(AplikacjaDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public virtual Task<TEntity> GetByIdAsync(TKey id, bool tracked = false)
        {
            IQueryable<TEntity> query;

            if (tracked)
                query = _dbSet;
            else
                query = _dbSet.AsNoTracking();

            return query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }
        public virtual TEntity Get(Expression<Func<TEntity, bool>> filter, string? includeProperties = null,
            bool tracked = false)
        {
            IQueryable<TEntity> query;

            if (tracked)
                query = _dbSet;
            else
                query = _dbSet.AsNoTracking();

            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }
        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter, string? includeProperties = null,
            bool tracked = false)
        {
            IQueryable<TEntity> query;

            if (tracked)
                query = _dbSet;
            else
                query = _dbSet.AsNoTracking();

            query = query.Where(filter);
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefaultAsync();
        }
        public virtual IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }
        public virtual Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToListAsync();
        }
        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
            _db.SaveChanges();
        }
        public virtual async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
        }
        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _db.SaveChangesAsync();
        }
        public virtual async Task RemoveAsync(TKey id)
        {
            var entity = await GetAsync(e => e.Id.Equals(id));
            entity.CzyAktywny = false;
            await _db.SaveChangesAsync();
        }
        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _db.SaveChangesAsync();
        }
        public virtual Task<bool> HasAnyAsync()
        {
            return _dbSet.AnyAsync();
        }
        public Task<bool> ExistsById(TKey id)
        {
            return _dbSet.AnyAsync(e => e.Id.Equals(id));
        }
        public Task<bool> Exists(TEntity entity)
        {
            return _dbSet.AnyAsync(e => e.Equals(entity));
        }
    }
}
