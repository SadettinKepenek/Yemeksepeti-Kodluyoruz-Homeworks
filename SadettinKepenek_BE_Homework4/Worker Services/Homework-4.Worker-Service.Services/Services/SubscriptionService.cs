using System.Linq;
using Homework_4.Worker_Service.Services.Data;
using Homework_4.Worker_Service.Services.Entites;
using Microsoft.EntityFrameworkCore;

namespace Homework_4.Worker_Service.Services.Services
{
    public class SubscriptionService:ISubscriptionService
    {
        private SubscriptionDbContext _subscriptionDbContext;

        public SubscriptionService()
        {
            _subscriptionDbContext = new SubscriptionDbContext();
        }

        public void Create(Subscription subscription)
        {
            _subscriptionDbContext.Subscriptions.Add(subscription);
            _subscriptionDbContext.SaveChangesAsync();
            _subscriptionDbContext.SubscriptionJobs.Add(new SubscriptionJob()
            {
                ExecutionDate = subscription.EndDate,
                IsExecuted = false,
                SubscriptionId = subscription.Id
            });
            _subscriptionDbContext.Entry(subscription).State = EntityState.Detached;
            _subscriptionDbContext.SaveChangesAsync();
        }
    }
}