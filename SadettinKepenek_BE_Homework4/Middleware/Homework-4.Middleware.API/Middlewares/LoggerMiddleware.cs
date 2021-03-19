using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Homework_4.Middleware.API.Enums;
using Homework_4.Middleware.Services.Services.Logger;
using Microsoft.AspNetCore.Http;
using LoggerFactory = Homework_4.Middleware.API.Factories.LoggerFactory;

namespace Homework_4.Middleware.API.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private LogBase _requestLogger;
        private LogBase _responseLogger;
        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
            _requestLogger = LoggerFactory.CreateLogger(LogTypes.RequestLog);
            _responseLogger = LoggerFactory.CreateLogger(LogTypes.ResponseLog);
        }

        public async Task Invoke(HttpContext context)
        {
            
            var requestId = Guid.NewGuid();

            var requestMessage = await FormatRequest(context.Request,requestId);

            var originalBodyStream = context.Response.Body;

            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            
            _requestLogger.Log(requestMessage);
            await _next(context);

            var responseMessage = await FormatResponse(context.Response,requestId);
            _responseLogger.Log(responseMessage);
            await responseBody.CopyToAsync(originalBodyStream);
        }
        
        private static async Task<string> FormatRequest(HttpRequest request,Guid requestId)
        {
            var body = request.Body;
            request.EnableBuffering();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            await request.Body.ReadAsync(buffer.AsMemory(0, buffer.Length)).ConfigureAwait(false);

            var bodyAsText = Encoding.UTF8.GetString(buffer);

            request.Body = body;

            return $"[{DateTime.Now}] RequestId:{requestId};Path:{request.Path};Protocol:{request.Protocol}";
        }

        private static async Task<string> FormatResponse(HttpResponse response,Guid RequestId)
        {
            
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return  $"[{DateTime.Now}] Id:{Guid.NewGuid()};RequestId:{RequestId};StatusCode:{response.StatusCode}";

        }
    }
}