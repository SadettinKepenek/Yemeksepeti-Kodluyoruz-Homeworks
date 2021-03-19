using System;

namespace Homework_4.Swagger.Infrastructure.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }

    }
}