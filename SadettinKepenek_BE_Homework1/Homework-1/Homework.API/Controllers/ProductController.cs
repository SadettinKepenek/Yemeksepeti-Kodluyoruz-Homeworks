using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Homework.Services.Product.Domain.Entities;
using Homework.Services.Product.Domain.Models;
using Homework.Services.Product.Domain.Repositories;
using Homework.Services.Product.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Homework.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(IMapper mapper, IRepository<Product, int> productRepository)
        {
            _productService = ProductService.GetInstance(mapper, productRepository);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _productService.Get(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            var result = await _productService.Create(dto);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut]
        public  IActionResult Update([FromBody] ProductDto dto)
        {
            var result = _productService.Update(dto);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result =  _productService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpOptions]
        public IActionResult Options()
        {
            HttpContext.Response.Headers.Add("Allow", "GET, PUT, POST, DELETE");
            return Ok(HttpContext.Response.Headers);
        }
    }
}