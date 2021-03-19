using System.Collections.Generic;
using Homework_4.Middleware.API.Enums;
using Homework_4.Middleware.Services.Services;
using Homework_4.Middleware.Services.Services.File;
using Homework_4.Middleware.Services.Services.Logger;

namespace Homework_4.Middleware.API.Factories
{
    public static class LoggerFactory
    {
        private static Dictionary<LogTypes, LogBase> _loggers;
        static LoggerFactory()
        {
            _loggers = new Dictionary<LogTypes, LogBase>();
            _loggers.Add(LogTypes.RequestLog,new RequestLogger(new FileService()));
            _loggers.Add(LogTypes.ResponseLog,new ResponseLogger(new FileService()));
        }
        
        public static LogBase CreateLogger(LogTypes logType)
        {
            return _loggers[logType];
        }
    }
}