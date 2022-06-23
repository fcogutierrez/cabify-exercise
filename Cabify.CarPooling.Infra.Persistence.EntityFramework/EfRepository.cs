#nullable enable
using Cabify.CarPooling.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Infra.Persistence.EntityFramework
{
    internal sealed class EfRepository<T> 
        : IRepository<T>
        where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;

        public EfRepository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public async Task<List<T>> ListAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> Add(T entity)
        {
            await _dbSet.AddAsync(entity);

            return entity;
        }

        public async Task AddRange(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<List<T>> Find(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null)
        {
            if (include == null)
            {
                return await _dbSet.Where(predicate).ToListAsync();
            }

            return await _dbSet.Where(predicate).Include(include).ToListAsync();
        }

        public async Task<T> FindOne(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null)
        {
            if (include == null)
            {
                return await _dbSet.SingleOrDefaultAsync(predicate);
            }

            return await _dbSet.Include(include).SingleOrDefaultAsync(predicate);
        }

        public void DeleteAll()
        {
            _dbSet.RemoveRange(_dbSet.Where(x => true));
        }
    }
}
