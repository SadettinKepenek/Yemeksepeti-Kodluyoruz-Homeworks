using Homework_4.Blog.Domain.Interfaces;

namespace Homework_4.Blog.Domain.Entities
{
    public class User:IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}