using Cabify.CarPooling.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cabify.CarPooling.Infra.Persistence
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        Task<List<T>> ListAll();
        Task<T> Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task<List<T>> Find(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null);
        Task<T> FindOne(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>? include = null);
        void DeleteAll();
    }
}