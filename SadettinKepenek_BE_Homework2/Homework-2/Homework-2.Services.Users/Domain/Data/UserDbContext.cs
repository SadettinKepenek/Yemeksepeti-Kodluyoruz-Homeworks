using Homework_2.Services.Users.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework_2.Services.Users.Domain.Data
{
    public class UserDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
    }
}