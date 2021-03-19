using Homework_4.Blog.Data.Repositories.Core;
using Homework_4.Blog.Data.Repositories.Interfaces;
using Homework_4.Blog.Domain.Entities;

namespace Homework_4.Blog.Data.Repositories.Derived
{
    public class UserRepository:RepositoryBase<User>,IUserRepository
    {
        
    }
}