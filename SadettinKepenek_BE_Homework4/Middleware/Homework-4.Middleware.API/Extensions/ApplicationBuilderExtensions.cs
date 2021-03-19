using Homework_4.Middleware.API.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Homework_4.Middleware.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseLogger(this IApplicationBuilder app )
        {
            app.UseMiddleware<LoggerMiddleware>();
            return app;
        }
    }
}