using System;
using System.Linq;
using Homework_4.Worker_Service.Services.Data;
using Homework_4.Worker_Service.Services.Entites;
using Microsoft.Extensions.Logging;

namespace Homework_4.Worker_Service.Services.Services
{
    public class SubscriptionJobService:ISubscriptionJobService
    {
        private readonly SubscriptionDbContext _dbContext;
        private readonly ILogger<SubscriptionService> _logger;
        public SubscriptionJobService(ILogger<SubscriptionService> logger)
        {
            _logger = logger;
            _dbContext = new SubscriptionDbContext();
        }
        
        public void RenewSubscriptions()
        {
            var subscriptionJobsToBeRenewed = _dbContext.SubscriptionJobs
                .Where(s => !s.IsExecuted && s.ExecutionDate <= DateTime.Now).ToList();
            
            foreach (var subscriptionJob in subscriptionJobsToBeRenewed)
            {
                var subscription = _dbContext.Subscriptions.FirstOrDefault(s => s.Id == subscriptionJob.SubscriptionId);
                
                subscription.StartDate = DateTime.Now;
                subscription.EndDate = DateTime.Now.AddDays(30);
                _dbContext.Subscriptions.Update(subscription);

                subscriptionJob.IsExecuted = true;
                _dbContext.SubscriptionJobs.Add(new SubscriptionJob()
                {
                    ExecutionDate = subscription.EndDate,
                    IsExecuted = false,
                    SubscriptionId = subscription.Id
                });
                _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Subscription with id {subscription.Id} renewed");
                
            }
        }
    }
}