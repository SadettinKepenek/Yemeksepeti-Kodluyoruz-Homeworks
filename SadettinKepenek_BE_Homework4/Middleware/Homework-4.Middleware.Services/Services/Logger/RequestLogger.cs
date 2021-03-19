using Homework_4.Middleware.Infrastructure.Constants;
using Homework_4.Middleware.Services.Services.File;

namespace Homework_4.Middleware.Services.Services.Logger
{
    public class RequestLogger:LogBase
    {
        private readonly IFileService _fileService;

        public RequestLogger(IFileService fileService)
        {
            _fileService = fileService;
        }

        public override void Log(string message)
        {
            _fileService.WriteToFile(message,FileConstants.RequestFileName);
        }
    }
}