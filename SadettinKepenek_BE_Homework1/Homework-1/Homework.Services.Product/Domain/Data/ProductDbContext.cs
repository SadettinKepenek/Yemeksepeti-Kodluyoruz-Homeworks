using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Homework.Services.Product.Domain.Data
{
    public class ProductDbContext:DbContext
    {
        private IConfiguration _configuration;
        public  DbSet<Entities.Product> Products { get; set; }

        public ProductDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ProductDbContext(DbContextOptions<ProductDbContext> options,
            IConfiguration configuration) :
            base(options)
        {
            _configuration = configuration;
        }

        public ProductDbContext()
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Product>()
                .Property(x => x.Id).ValueGeneratedOnAdd();
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetValue<string>("ProductConnectionString");
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(connectionString);
        }
    }
}