using Homework_4.Whitelist.API.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Homework_4.Whitelist.API.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIpRestricter(this IApplicationBuilder app )
        {
            app.UseMiddleware<IpRestricterMiddleware>();
            return app;
        }
    }
}