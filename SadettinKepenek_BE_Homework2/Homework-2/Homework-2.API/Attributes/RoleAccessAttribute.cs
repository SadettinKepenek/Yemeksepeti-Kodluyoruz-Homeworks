using System;
using System.Security.Authentication;
using Homework_2.Services.Users.Domain.Entities;
using Homework_2.Services.Users.Domain.Enums;
using Homework_2.Services.Users.Domain.Models;
using Homework_2.Services.Users.Domain.Sessions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Homework_2.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RoleAccessAttribute : ActionFilterAttribute
    {
        public UserRole Role { get; set; }

        public RoleAccessAttribute(UserRole role)
        {
            Role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var isAuthorized = IsAuthorized(context);
            if (!isAuthorized)
            {
                throw new UnauthorizedAccessException("You dont have access");
            }
        }

        private bool IsAuthorized(ActionExecutingContext context)
        {
            var userString = context.HttpContext.Session.GetString("User");
            if (userString == null)
            {
                throw new AuthenticationException("Please login inorder to use this endpoint");
            }

            var user = JsonConvert.DeserializeObject<UserSession>(userString);
            return user.RoleId == (int) Role;
        }
    }
}