using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Homework_4.Middleware.Services.Services.File
{
    public class FileService : IFileService
    {
        public void WriteToFile(string message, string fileName)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + $"/{fileName}";
            if (!IsFileExist(path))
            {
                using var file = System.IO.File.Create(path);
            }
            
            using var fileWriter = new StreamWriter(path,true);
            fileWriter.WriteLine(message);
            fileWriter.Close();
        }

        public List<string> ReadFile(string fileName)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + $"/{fileName}";
            var file = System.IO.File.ReadAllLines(path);
            return file.ToList();
        }

        private bool IsFileExist(string path)
        {
            return System.IO.File.Exists(path);
        }
    }
}