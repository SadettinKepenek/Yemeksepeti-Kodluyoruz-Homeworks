using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Homework_4.Blog.Domain.Interfaces;

namespace Homework_4.Blog.Data.Repositories.Core
{
    public interface IReadRepository<T> where T: IEntity
    {
        Task<T> GetById(int id);

        Task<T> Get(Expression<Func<T, bool>> expression);

        Task<IQueryable<T>> GetAll();

        Task<IQueryable<T>> GetMany(Expression<Func<T, bool>> expression);
    }
}