using System.Collections.Generic;

namespace Homework_4.Middleware.Services.Services.File
{
    public interface IFileService
    {
        void WriteToFile(string message,string fileName);
        List<string> ReadFile(string fileName);
    }
}