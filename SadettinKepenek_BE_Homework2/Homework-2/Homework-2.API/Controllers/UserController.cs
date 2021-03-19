using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Homework_2.API.Attributes;
using Homework_2.Services.Users.Domain.Enums;
using Homework_2.Services.Users.Domain.Requests;
using Homework_2.Services.Users.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Homework_2.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            var response = _userService.Register(request);
            if (!response.Status)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var response = _userService.Login(request);
            if (!response.Status)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet]
        [RoleAccess(UserRole.Admin)]
        public IActionResult GetAll()
        {
            HttpContext.Session.GetString("User");
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}