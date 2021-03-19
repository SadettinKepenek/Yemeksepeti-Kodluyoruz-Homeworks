using System.Threading.Tasks;
using Homework_4.Swagger.Infrastructure.Models;
using Homework_4.Swagger.Services.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Homework_4.Swagger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController:ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderDto dto)
        {
            var result = await _orderService.Create(dto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _orderService.GetById(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAll();
            return Ok(result);
        }
    }
}