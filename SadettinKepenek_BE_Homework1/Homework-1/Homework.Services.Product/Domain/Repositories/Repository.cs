using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Homework.Services.Product.Domain.Data;
using Homework.Services.Product.Domain.Entities.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Homework.Services.Product.Domain.Repositories
{
    public class Repository<T,TKey> : IRepository<T,TKey> where T : class, IEntity<TKey>
    {
        protected readonly ProductDbContext Context;
        private readonly DbSet<T> _entities;
        public Repository(ProductDbContext context)
        {
            Context = context;
            _entities = Context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {

            await _entities.AddAsync(entity);
            await Context.SaveChangesAsync();
        }
        
        public async Task<List<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetByIdAsync(TKey id)
        {
            return await _entities.FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public IQueryable<T> GetQueryable()
        {
            return _entities.AsQueryable().AsNoTracking();
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
            Context.SaveChangesAsync();
        }

        public void  Update(T entity)
        {
            _entities.Update(entity);
            Context.SaveChangesAsync();
        }

    }
}