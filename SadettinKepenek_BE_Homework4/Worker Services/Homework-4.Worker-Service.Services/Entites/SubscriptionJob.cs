using System;

namespace Homework_4.Worker_Service.Services.Entites
{
    public class SubscriptionJob
    {
        public int Id { get; set; }
        public DateTime ExecutionDate { get; set; }
        public bool IsExecuted { get; set; }
        public int SubscriptionId { get; set; }
    }
}