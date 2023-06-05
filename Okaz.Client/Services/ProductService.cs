using Okaz.API.Models.DTOs;

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
  }
}