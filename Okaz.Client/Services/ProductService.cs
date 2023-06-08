using Okaz.API.Models.DTOs;
using Okaz.Models;
namespace Okaz.Client.Services
{
  public class ProductService : IProductService
  {
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
      _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductDTO>> GetProducts()
    {
      return await _httpClient.GetFromJsonAsync<ProductDTO[]>("api/products");
    }
   
    public async Task<ProductDTO> GetProductById(int id){
      return await _httpClient.GetFromJsonAsync<ProductDTO>($"api/products/{id}");
    }
    // public async Task<ProductCreateDTO> UpdateProduct(ProductCreateDTO updatedProduct)
    // {
    //   return await _httpClient.PutAsJsonAsync<Product> ($"api/products", updatedProduct);
    // }
    // public async Task<ProductCreateDTO> ProductCreateDTO(ProductCreateDTO request)
    // {
    //   return _httpClient.PutAsAsync<ProductCreateDTO> ($"api/products/{id}");
    // }
    public async Task<Product> DeleteProduct(int id){
      return await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");
    }

  }
}