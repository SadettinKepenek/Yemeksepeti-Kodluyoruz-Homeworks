using Homework_4.Jwt.API.Abstract;

namespace Homework_4.Jwt.API.Models.Derived
{
    public class HotelInfo:Resource
    {
        public string Title {get;set;}
        public string Email {get;set;}
        public string WebSite {get;set;}
        public HotelAddress Location {get;set;}
    }
}