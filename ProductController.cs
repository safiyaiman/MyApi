using Microsoft.AspNetCore.Mvc;

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
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                if (product == null)
                {
                    return NotFound(new { message = $"Product with id {id} was not found." });
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, 
                    new { message = "An error occurred while processing the request.", detail = ex.Message });
            }
                
                
        }
  
}