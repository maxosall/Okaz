using Okaz.API.Models.DTOs;
using Okaz.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Net;


namespace Okaz.Client.Services;

public class ProductService : IProductService
{
  private readonly HttpClient _httpClient;

  public ProductService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<IEnumerable<ProductDTO>> GetProducts()
  {
    try
    {

      return await _httpClient.GetFromJsonAsync<ProductDTO[]>("api/products");
    }
    catch (Exception ex)
    {
      throw;
    }
  }

  public async Task<ProductDTO> GetProductById(int id)
  {
    try
    {
      var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
      // Use ConfigureAwait(false) to avoid blocking the UI thread
      return await _httpClient.GetFromJsonAsync<ProductDTO>($"api/products/{id}", 
        cancellationTokenSource.Token)
          .ConfigureAwait(false);
    }
    catch (HttpRequestException ex)
    {
      // Handle HTTP exceptions such as 404, 500, etc.
      Console.WriteLine($"An error occurred while requesting product {id}: {ex.Message}");
      return null;
    }
    catch (OperationCanceledException)
    {
      // Handle the case when the request is canceled by the token
      Console.WriteLine($"The request for product {id} was canceled due to timeout.");
      return null;
    }
    catch (Exception ex)
    {
      // Handle any other unexpected exceptions
      Console.WriteLine($"An unexpected error occurred while requesting product {id}: {ex.Message}");
      return null;
    }
  }



  public async Task<Product> UpdateProduct(ProductCreateDTO updatedProduct)
  {
    try
    {
      HttpResponseMessage response = await _httpClient
        .PutAsJsonAsync<ProductCreateDTO> ("api/products", updatedProduct);

      if (!response.IsSuccessStatusCode) 
      {     
        throw GetExceptionForStatusCode(response.StatusCode, updatedProduct.ProductId);
      }
      return await response.Content.ReadFromJsonAsync<Product>();  
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex.Message);
      return null;
    }
  }

  // public async Task<ProductCreateDTO> ProductCreateDTO(ProductCreateDTO request)
  // {
  //   return _httpClient.PutAsAsync<ProductCreateDTO> ($"api/products/{id}");
  // }
  public async Task<Product> DeleteProduct(int id)
  {
    return await _httpClient.GetFromJsonAsync<Product>($"api/products/{id}");
  }

  private Exception GetExceptionForStatusCode(HttpStatusCode statusCode, int productId)
  {
    return statusCode switch
    {
      HttpStatusCode.NotFound => new Exception($"No product with {productId} was found"),
      HttpStatusCode.BadRequest => new Exception("The request was invalid or malformed"),
      _ => new Exception("An error occurred while updating the product")
    };
  }
}