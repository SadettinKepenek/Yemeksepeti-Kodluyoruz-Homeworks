namespace Homework_4.Blog.Domain.Models
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
    }
}