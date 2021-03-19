using Homework.Services.Product.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Homework.API.Extensions
{
    public static class DbConfigurer
    {
        public static void ConfigureDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("ProductConnectionString");
            services.AddDbContext<ProductDbContext>(opt => { opt.UseSqlServer(connectionString);});
        }
    }
}