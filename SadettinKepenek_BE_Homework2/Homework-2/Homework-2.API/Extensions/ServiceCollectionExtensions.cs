using Homework_2.Services.Users.Domain.Data;
using Homework_2.Services.Users.Domain.Services;
using Homework_2.Services.Users.Domain.Sessions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Homework_2.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDb(this IServiceCollection services)
        {
            services.AddDbContext<UserDbContext>(builder => { builder.UseInMemoryDatabase("UserDb"); });
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService,UserService>();
            services.AddHttpContextAccessor();
            services.AddScoped<ISessionManager,SessionManager>();
        }
    }
}