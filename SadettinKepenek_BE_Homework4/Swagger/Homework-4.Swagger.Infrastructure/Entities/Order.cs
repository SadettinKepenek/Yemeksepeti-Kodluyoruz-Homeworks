using System;

namespace Homework_4.Swagger.Infrastructure.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public string OrderCode { get; set; }
    }
}