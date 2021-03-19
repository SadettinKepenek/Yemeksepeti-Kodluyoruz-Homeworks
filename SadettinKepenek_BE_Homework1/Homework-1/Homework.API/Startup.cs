using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Homework.API.Extensions;
using Homework.Services.Product.Domain.Mappers;
using Homework.Services.Product.Domain.Models;
using Homework.Services.Product.Domain.Repositories;
using Homework.Services.Product.Domain.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Homework.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(setup => {
            }).AddFluentValidation();
            services.AddAutoMapper(typeof(ProductProfile));
            services.ConfigureDb(Configuration);
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddTransient<IValidator<ProductDto>, ProductDtoValidator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}