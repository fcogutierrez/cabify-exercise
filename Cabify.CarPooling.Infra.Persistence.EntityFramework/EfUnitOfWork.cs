using Cabify.CarPooling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Infra.Persistence.EntityFramework
{
    internal sealed class EfUnitOfWork
        : IUnitOfWork
    {
        private readonly CabifyDbContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public EfUnitOfWork(CabifyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<T>> ListAll<T>()
            where T : BaseEntity
        {
            return Repository<T>().ListAll();
        }

        public Task<T> Add<T>(T entity)
            where T : BaseEntity
        {
            return Repository<T>().Add(entity);
        }

        public Task AddRange<T>(IEnumerable<T> entities)
            where T : BaseEntity
        {
            return Repository<T>().AddRange(entities);
        }


        public Task<List<T>> Find<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null)
            where T : BaseEntity
        {
            return Repository<T>().Find(predicate, include);
        }

        public Task<T> FindOne<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null)
            where T : BaseEntity
        {
            return Repository<T>().FindOne(predicate, include);
        }

        public void DeleteAll<T>()
            where T : BaseEntity
        {
            Repository<T>().DeleteAll(); ;
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }

        private IRepository<T> Repository<T>() where T : BaseEntity
        {
            if (!_repositories.TryGetValue(typeof(T), out var repository))
            {
                repository = new EfRepository<T>(_dbContext.Set<T>());

                _repositories.Add(typeof(T), repository);
            }

            return (IRepository<T>)repository;
        }
    }
}