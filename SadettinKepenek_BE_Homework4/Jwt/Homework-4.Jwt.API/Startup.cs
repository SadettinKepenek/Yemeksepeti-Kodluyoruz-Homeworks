using System.Text;
using Homework_4.Jwt.API.Data;
using Homework_4.Jwt.API.Filters;
using Homework_4.Jwt.API.Infrastructure;
using Homework_4.Jwt.API.Models.Derived;
using Homework_4.Jwt.API.Services;
using Homework_4.Jwt.API.Workers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Homework_4.Jwt.API
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
            
            services.Configure<HotelInfo>(
                Configuration.GetSection("HotelInfo")
            );

            services.AddDbContext<HotelApiDbContext>(options => 
            {
                options.UseInMemoryDatabase("HotelDB");
            });

            services.AddControllers(options => 
            {
                options.Filters.Add(typeof(JsonExceptionFilters));
                options.Filters.Add<AllowOnlyRequireHttp>();
            });

            //services.AddAutoMapper(typeof(MappingProfile));
            services.AddAutoMapper(option => option.AddProfile<MappingProfile>());
            services.AddScoped<IRoomService,RoomService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddSession()


            string key  = Configuration.GetValue<string>("JwtTokenKey");
            byte[] keyValue = Encoding.UTF8.GetBytes(key);

            services.AddAuthentication(auth => 
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt => {
                jwt.RequireHttpsMetadata = true;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyValue),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddHostedService<RoomWorkerService>();

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUi3();
                app.UseOpenApi();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}