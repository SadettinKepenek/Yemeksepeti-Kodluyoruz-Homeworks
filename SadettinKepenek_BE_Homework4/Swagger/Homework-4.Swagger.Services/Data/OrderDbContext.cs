using Homework_4.Swagger.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Swagger.Services.Data
{
    public class OrderDbContext:DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public OrderDbContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
            
        }
    }
}