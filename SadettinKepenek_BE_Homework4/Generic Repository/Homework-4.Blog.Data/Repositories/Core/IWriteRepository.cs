using System.Threading.Tasks;
using Homework_4.Blog.Domain.Interfaces;

namespace Homework_4.Blog.Data.Repositories.Core
{
    public interface IWriteRepository<T> where T:IEntity
    {
        Task<T> Add(T entity);

        Task<T> Update(T entity);

        Task<bool> Delete(T entity);

        Task<bool> Delete(int id);
    }
}