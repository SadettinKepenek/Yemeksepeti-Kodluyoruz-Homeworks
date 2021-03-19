using Microsoft.AspNetCore.Mvc;

namespace Homework_4.Jwt.API.Models
{
    public class ApiError
    {
        public string Message {get;set;}
        public string Detail {get;set;}
        public ApiVersion Version {get;set;}
    }
}