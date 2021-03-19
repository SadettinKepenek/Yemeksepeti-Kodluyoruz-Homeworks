using Homework_4.Whitelist.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Whitelist.API.Data
{
    public class DataContext:DbContext
    {
        public DbSet<UserIp> UserIps { get; set; }
        public DbSet<UserRestriction> UserRestrictions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("WhiteListDb");
            base.OnConfiguring(optionsBuilder);
        }
    }
}