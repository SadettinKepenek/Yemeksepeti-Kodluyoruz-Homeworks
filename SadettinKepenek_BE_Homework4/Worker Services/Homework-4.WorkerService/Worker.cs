using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Homework_4.Worker_Service.Services.Entites;
using Homework_4.Worker_Service.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Homework_4.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISubscriptionService _subscriptionService;
        private readonly ISubscriptionJobService _subscriptionJobService;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            using var scope = serviceProvider.CreateScope();
            _subscriptionService = scope.ServiceProvider.GetRequiredService<ISubscriptionService>();
            _subscriptionJobService = scope.ServiceProvider.GetRequiredService<ISubscriptionJobService>();
            SeedData();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _subscriptionJobService.RenewSubscriptions();
                await Task.Delay(3000, stoppingToken);
            }
        }

        private void SeedData()
        {
            for (int i = 1; i < 11; i++)
            {
                _subscriptionService.Create(new Subscription()
                {
                    Id = i,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddSeconds(i * 10)
                });
            }
        }
    }
}