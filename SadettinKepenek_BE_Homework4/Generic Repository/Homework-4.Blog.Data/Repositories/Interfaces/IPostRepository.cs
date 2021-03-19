using Homework_4.Blog.Data.Repositories.Core;
using Homework_4.Blog.Domain.Entities;

namespace Homework_4.Blog.Data.Repositories.Interfaces
{
    public interface IPostRepository:IReadRepository<Post>,IWriteRepository<Post>
    {
        
    }
}