using System;

namespace Homework_4.Swagger.Infrastructure.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public string OrderCode { get; set; }
    }
}