using System.Collections.Generic;
using Homework_4.Swagger.Services.Data;
using Homework_4.Swagger.Services.MappingProfiles;
using Homework_4.Swagger.Services.Services.Abstract;
using Homework_4.Swagger.Services.Services.Derivered;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Homework_4.Swagger.IoC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddAutoMapper(typeof(MappingProfile));
            return services;    
        }
        public static IServiceCollection ConfigureDb(this IServiceCollection services)
        {
            services.AddDbContext<OrderDbContext>(builder => { builder.UseInMemoryDatabase("UserDb"); });
            return services;
        }
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Ordering API",
                        Description = "Ordering API End Points",
                    });

                    c.IgnoreObsoleteActions();

                });
                return services;
            }
        
    }
}