namespace Homework_4.Swagger.Infrastructure.Models
{
    public class ServiceResponseModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }

        public ServiceResponseModel(string message, bool success)
        {
            Message = message;
            Success = success;
        }
    }
}