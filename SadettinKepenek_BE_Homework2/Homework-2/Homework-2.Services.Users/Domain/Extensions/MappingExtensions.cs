using System.Collections.Generic;
using System.Linq;
using Homework_2.Services.Users.Domain.Entities;
using Homework_2.Services.Users.Domain.Models;
using Homework_2.Services.Users.Domain.Requests;

namespace Homework_2.Services.Users.Domain.Extensions
{
    public static class MappingExtensions
    {
        public static User MapToUser(this RegisterRequest request)
        {
            return new User()
            {
                Email = request.Email,
                Password = request.Password,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                RoleId = request.RoleId
            };
        } 
        public static User MapToUser(this LoginRequest request)
        {
            return new User()
            {
                Email = request.Email,
                Password = request.Password
            };
        }
        
        public static List<UserDto> MapToUserDto(this List<User> users)
        {
            return users.Select(u => new UserDto()
            {
                Email = u.Email,
                Firstname = u.Firstname,
                Lastname = u.Lastname,
                Id = u.Id
            }).ToList();
        }
    }
}