using System;

namespace Homework_4.Jwt.API.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime ExpireTime { get; set; }
        public string Token { get; set; }
    }
}