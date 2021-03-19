namespace Homework_2.Services.Users.Domain.Responses
{
    public class ServiceResponseModel
    {
        public string Message { get; set; }
        public bool Status { get; set; }

        public ServiceResponseModel(string message, bool status)
        {
            Message = message;
            Status = status;
        }
    }
}