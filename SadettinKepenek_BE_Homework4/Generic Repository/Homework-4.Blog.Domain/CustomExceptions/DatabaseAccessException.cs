using System;

namespace Homework_4.Blog.Domain.CustomExceptions
{
    public class DatabaseAccessException:Exception
    {
        public DatabaseAccessException()
        {
        }

        public DatabaseAccessException(string message) : base(message)
        {
        }

        public DatabaseAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}