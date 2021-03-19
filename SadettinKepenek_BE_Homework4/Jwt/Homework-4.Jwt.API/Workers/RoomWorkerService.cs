using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Homework_4.Jwt.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Homework_4.Jwt.API.Workers
{
    public class RoomWorkerService: BackgroundService
    {
         private ILogger<RoomWorkerService> _logger;

        private readonly IServiceScopeFactory _scopeFactory;

        private HotelApiDbContext _dbContext;

        public RoomWorkerService(ILogger<RoomWorkerService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {

            var scope = _scopeFactory.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<HotelApiDbContext>();

            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);

        }

        public override void Dispose()
        {
            base.Dispose();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // definitions 

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_dbContext == null)
                {
                    var scope = _scopeFactory.CreateScope();
                    _dbContext = scope.ServiceProvider.GetRequiredService<HotelApiDbContext>();
                }


                var migratingrecords = await _dbContext.Rooms
                                                      .Where(room => !room.IsMigrate).ToListAsync();

 
                foreach (var record in migratingrecords)
                {

                    record.IsMigrate = true;
                }

                if (_dbContext.ChangeTracker.HasChanges())
                    await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Worker Runing");



                await Task.Delay(3000, stoppingToken);

            }


        }
    }
}