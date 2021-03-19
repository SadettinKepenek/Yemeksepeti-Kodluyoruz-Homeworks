using System.ComponentModel.DataAnnotations;
using Homework_2.Services.Users.Domain.Enums;

namespace Homework_2.Services.Users.Domain.Requests
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$",ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "RoleId is required")]
        [Range(0,1,ErrorMessage = "Please enter a invalid roleId.Admin=0,User=1")]
        public int RoleId { get; set; }
    }
}