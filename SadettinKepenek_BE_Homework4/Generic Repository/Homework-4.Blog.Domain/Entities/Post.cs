using Homework_4.Blog.Domain.Interfaces;

namespace Homework_4.Blog.Domain.Entities
{
    public class Post:IEntity
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}