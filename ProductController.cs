using Microsoft.AspNetCore.Mvc;
using MyApi.Models;

namespace MyApi;
   
    [ApiController]
    [Route("api/[controller]")]
public class ProductController : ControllerBase
{

        private readonly IProducts _productService;

        public ProductController(IProducts productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
                var data = await _productService.GetProductsAsync();
                return Ok(data);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }
  
}