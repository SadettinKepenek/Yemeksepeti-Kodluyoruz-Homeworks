using Homework_4.Blog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Blog.Data.Context
{
    public class DataContext:DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("BlogDb");
            base.OnConfiguring(optionsBuilder);
        }
    }
}