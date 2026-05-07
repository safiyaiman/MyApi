using MyApi.Models;

namespace MyApi;

public interface IProducts
{
   Task<List<Product>?> GetProductsAsync();
   Task<Product?> GetProductByIdAsync(int id);
}