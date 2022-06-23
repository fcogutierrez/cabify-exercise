using Cabify.CarPooling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Infra.Persistence
{
    public interface IUnitOfWork
    {
        Task<List<T>> ListAll<T>() 
            where T : BaseEntity;

        Task<T> Add<T>(T entity)
            where T : BaseEntity;

        Task AddRange<T>(IEnumerable<T> entities)
            where T : BaseEntity;

        Task<List<T>> Find<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null)
            where T : BaseEntity;

        Task<T> FindOne<T>(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null)
            where T : BaseEntity;

        void DeleteAll<T>()
            where T : BaseEntity;

        Task<int> SaveChanges();
    }
}