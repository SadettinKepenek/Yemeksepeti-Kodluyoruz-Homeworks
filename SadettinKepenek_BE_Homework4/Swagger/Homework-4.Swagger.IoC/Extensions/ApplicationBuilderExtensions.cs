using Microsoft.AspNetCore.Builder;

namespace Homework_4.Swagger.IoC.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder BuildSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });
            return app;
        }
    }
}