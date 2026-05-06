using Microsoft.AspNetCore.Mvc;
using MyApi.Models;

namespace MyApi;
    [Route("api/[controller]/[action]")]
    [ApiController]
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
            try
            {
                var data = await _productService.GetProductsAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString()); // temporary for debugging
            }
        }
  
}