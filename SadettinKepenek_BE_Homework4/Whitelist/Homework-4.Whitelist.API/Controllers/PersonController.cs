using Microsoft.AspNetCore.Mvc;

namespace Homework_4.Whitelist.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController:ControllerBase
    {
        public IActionResult Get()
        {
            return Ok("Success");
        }
    }
}