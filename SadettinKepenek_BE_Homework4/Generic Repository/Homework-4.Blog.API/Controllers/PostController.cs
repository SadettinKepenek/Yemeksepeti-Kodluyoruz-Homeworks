using System.Threading.Tasks;
using Homework_4.Blog.Domain.Models;
using Homework_4.Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4.Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController:ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _postService.GetById(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _postService.GetAll();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostDto postDto)
        {
            var result = await _postService.Create(postDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] PostDto postDto)
        {
            var result = await _postService.Update(postDto);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}