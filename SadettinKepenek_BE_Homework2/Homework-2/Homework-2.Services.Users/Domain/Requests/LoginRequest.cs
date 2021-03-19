using System.ComponentModel.DataAnnotations;

namespace Homework_2.Services.Users.Domain.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}