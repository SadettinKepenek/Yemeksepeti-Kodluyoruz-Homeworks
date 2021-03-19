using System;

namespace Homework_4.Singleton_Transient_Scoped.API.Services
{
    public class RandomNumberService : IScopedService, ISingletonService, ITransientService
    {
        public int RandomNumber { get; set; }

        public RandomNumberService()
        {
            var rand = new Random();
            RandomNumber = rand.Next(1, 1000);
        }

        public int GetRandomNumber()
        {
            return RandomNumber;
        }

    }
}