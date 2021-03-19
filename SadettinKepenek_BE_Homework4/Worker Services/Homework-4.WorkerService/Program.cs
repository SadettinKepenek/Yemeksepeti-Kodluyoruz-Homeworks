using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Homework_4.Worker_Service.Services.Data;
using Homework_4.Worker_Service.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Homework_4.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddScoped<ISubscriptionService,SubscriptionService>();
                    services.AddScoped<ISubscriptionJobService,SubscriptionJobService>();
                    services.AddDbContext<SubscriptionDbContext>();
                });
    }
}