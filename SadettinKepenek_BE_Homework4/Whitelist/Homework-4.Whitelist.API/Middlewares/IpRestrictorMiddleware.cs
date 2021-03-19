using System;
using System.Threading.Tasks;
using Homework_4.Whitelist.API.Entities;
using Homework_4.Whitelist.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Homework_4.Whitelist.API.Middlewares
{
    public class IpRestricterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ISecurityService _securityService;
        private readonly IUserRestrictionService _userRestrictionService;

        public IpRestricterMiddleware(RequestDelegate next, IServiceProvider _serviceProvider)
        {
            _next = next;
            using var scope = _serviceProvider.CreateScope();
            _securityService = scope.ServiceProvider.GetRequiredService<ISecurityService>();
            _userRestrictionService = scope.ServiceProvider.GetRequiredService<IUserRestrictionService>();
            SeedData();
        }

        private void SeedData()
        {
            _userRestrictionService.AddUserIp(new UserIp()
            {
                Id = 1,
                IpAddress = "::1"
            });
            _userRestrictionService.AddUserIp(new UserIp()
            {
                Id = 2,
                IpAddress = "192.168.1.2"
            });
            _userRestrictionService.AddUserRestriction(new UserRestriction()
            {
                Id = 1,
                ControllerName = "home",
                UserIpId = 1
            });
            _userRestrictionService.AddUserRestriction(new UserRestriction()
            {
                Id = 2,
                ControllerName = "customer",
                UserIpId = 1
            });
            _userRestrictionService.AddUserRestriction(new UserRestriction()
            {
                Id = 3,
                ControllerName = "person",
                UserIpId = 2
            });
        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress.ToString();
            var routeData = context.Request.Path.ToString().Split("/");
            if (routeData.Length <= 2)
            {
                throw new InvalidOperationException("Url Not Found ");
            }
            var controllerName = routeData[2];
            var canAccessController = _securityService.CanAccessController(ipAddress, controllerName);
            if (!canAccessController)
            {
                throw new UnauthorizedAccessException("You dont have permission to access this controller");
            }

            await _next(context);
        }
    }
}