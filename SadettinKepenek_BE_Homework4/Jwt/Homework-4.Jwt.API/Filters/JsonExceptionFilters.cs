  
using System.Reflection.Metadata;
using System;
using Homework_4.Jwt.API.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
namespace Homework_4.Jwt.API.Filters
{
    public class JsonExceptionFilters:IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;

        public JsonExceptionFilters(IWebHostEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var isDevelopment = _env.IsDevelopment();

            var err = new ApiError
            {
                Version = context.HttpContext.GetRequestedApiVersion(),
                Message = isDevelopment ?  context.Exception.Message : "Api Error",
                Detail = isDevelopment ? context.Exception.StackTrace :context.Exception.Message
            };

            context.Result = new ObjectResult(err) { StatusCode = 500 };

        }
    }
}