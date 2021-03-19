using Microsoft.AspNetCore.Mvc;

namespace Homework_4.Middleware.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {

        [HttpGet]
        public IActionResult GetProducts()
        {
            //db işlemi
            //business vs..

            return Ok("Products has been listed");
        }
        
        [HttpPost("InsertProduct")]
        public IActionResult InsertProduct()
        {
            //db işlemi
            //business vs..

            return Ok("Product has been inserted");
        }
        
    }
}