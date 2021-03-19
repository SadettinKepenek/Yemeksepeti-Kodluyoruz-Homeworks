using System.Collections.Generic;
using System.Linq;
using Homework_2.Services.Users.Domain.Data;
using Homework_2.Services.Users.Domain.Extensions;
using Homework_2.Services.Users.Domain.Models;
using Homework_2.Services.Users.Domain.Requests;
using Homework_2.Services.Users.Domain.Responses;
using Homework_2.Services.Users.Domain.Sessions;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace Homework_2.Services.Users.Domain.Services
{
    public class UserService:IUserService
    {
        private UserDbContext _dbContext;
        private readonly ISessionManager _sessionManager;
        public UserService(UserDbContext dbContext, ISessionManager sessionManager)
        {
            _dbContext = dbContext;
            _sessionManager = sessionManager;
        }

        public ServiceResponseModel Register(RegisterRequest request)
        {
            var isUserExist = IsUserExist(request.Email);
            if (isUserExist)
            {
                return new ServiceResponseModel("User is already exist",false);
            }
            var user = request.MapToUser();
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return new ServiceResponseModel("User registered successfully",true);

        }

        private bool IsUserExist(string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Email.Equals(email));
            return user != null;
        }
        public ServiceResponseModel Login(LoginRequest request)
        {
            var user  = _dbContext.Users.FirstOrDefault(u =>
                u.Email.Equals(request.Email) && u.Password.Equals(request.Password));
            
            if (user != null)
            {
                var sessionData = JsonConvert.SerializeObject(new UserSession()
                {
                    Email = user.Email,
                    RoleId = user.RoleId
                });
                _sessionManager.SetSession("User", sessionData);
                return new ServiceResponseModel("Login success",false);
            }
            return new ServiceResponseModel("Email or password is not correct",false);

        }

        public List<UserDto> GetAll()
        {
            var users = _dbContext.Users.ToList();
            return users.MapToUserDto();
        }
    }
}