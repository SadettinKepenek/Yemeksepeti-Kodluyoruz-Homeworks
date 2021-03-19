using System.Threading.Tasks;
using Homework_4.Blog.Domain.Models;
using Homework_4.Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4.Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _userService.GetById(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            var result = await _userService.Create(userDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserDto userDto)
        {
            var result = await _userService.Update(userDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}