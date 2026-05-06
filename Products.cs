using System.Net;
using System.Text.Json;
using MyApi.Models;
using MyApi.Repositories;


namespace MyApi;

public class Products : IProducts
{
    private static readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://dummyjson.com/")
    };

    private readonly ProductRepository _productRepository;

    public Products(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task<List<Product>?> GetProductsAsync()
    {
        try
        {
            var local = await _productRepository.GetAllAsync();
            if (local.Count > 0) 
                return local;
            
            var result = new List<Product>();
            var response = await httpClient.GetAsync("products");

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadFromJsonAsync<ProductsApiResponse>();
                result= apiResponse?.Products ?? new List<Product>();

                if (result.Count > 0)
                {
                    await _productRepository.InsertProductsAsync(result);
                }

            }
            else
            {
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception("Products not found.");
                }
                else
                {
                    throw new Exception("Failed to fetch data from the server." +
                                        " Status code: " + response.StatusCode);
                }
            }
            return result;
        }
        catch (HttpRequestException ex)
        {
            throw new Exception("HTTP request failed: " + ex.Message);
        }
        catch (JsonException ex)
        {
            throw new Exception("JSON deserialization failed: " + ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error occurred: " + ex.Message);
        }
    }
    
}