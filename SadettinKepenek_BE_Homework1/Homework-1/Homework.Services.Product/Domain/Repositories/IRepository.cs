using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Homework.Services.Product.Domain.Entities.Abstract;

namespace Homework.Services.Product.Domain.Repositories
{
    public interface IRepository<TEntity,TKey> where TEntity:IEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id);
        Task<List<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        IQueryable<TEntity> GetQueryable();
    }
}