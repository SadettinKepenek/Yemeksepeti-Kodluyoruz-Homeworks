using Newtonsoft.Json;

namespace Homework_4.Jwt.API.Abstract
{
    public class Resource
    {
        [JsonProperty(Order = -1)]
        public string Href {get;set;}
    }
}