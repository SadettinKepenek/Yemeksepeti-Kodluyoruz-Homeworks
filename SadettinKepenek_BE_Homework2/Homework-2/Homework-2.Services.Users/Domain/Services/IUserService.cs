using System.Collections.Generic;
using Homework_2.Services.Users.Domain.Models;
using Homework_2.Services.Users.Domain.Requests;
using Homework_2.Services.Users.Domain.Responses;

namespace Homework_2.Services.Users.Domain.Services
{
    public interface IUserService
    {
        ServiceResponseModel Register(RegisterRequest request);
        ServiceResponseModel Login(LoginRequest request);
        List<UserDto> GetAll();
    }
}