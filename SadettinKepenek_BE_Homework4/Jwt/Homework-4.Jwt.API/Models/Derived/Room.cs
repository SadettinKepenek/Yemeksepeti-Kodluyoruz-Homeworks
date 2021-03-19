using Homework_4.Jwt.API.Abstract;

namespace Homework_4.Jwt.API.Models.Derived
{
    public class Room:Resource
    {
        public string Name { get; set; }
        public int  Rate { get; set; }
    }
}