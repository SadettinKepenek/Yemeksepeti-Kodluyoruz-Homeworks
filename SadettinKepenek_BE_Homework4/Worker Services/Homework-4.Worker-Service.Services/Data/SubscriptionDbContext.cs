using Homework_4.Worker_Service.Services.Entites;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Worker_Service.Services.Data
{
    public class SubscriptionDbContext:DbContext
    {
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionJob> SubscriptionJobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("SubscriptionDb");
            base.OnConfiguring(optionsBuilder);
        }
    }
}