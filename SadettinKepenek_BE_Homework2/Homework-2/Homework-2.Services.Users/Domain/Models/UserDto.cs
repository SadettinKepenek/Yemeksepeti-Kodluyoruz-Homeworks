using Homework_2.Services.Users.Domain.Enums;

namespace Homework_2.Services.Users.Domain.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public UserRole Role { get; set; }
    }
}