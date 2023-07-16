using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Okaz.API.Models.DTOs;
using Okaz.Models;

namespace Okaz.Client.Services;
public class CategoryService : ICategoryService
{
  private readonly HttpClient _httpClient;

  public CategoryService(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }

  public async Task<IEnumerable<CategoryDTO>> GetCategories()
  {
    try
    {
      return await _httpClient.GetFromJsonAsync<IEnumerable<CategoryDTO>>("api/category");
    }
    catch (Exception ex)
    {
      throw;
    }
  }
  public async Task<CategoryDetailsDTO> GetCategoyById(int id)
  {
    try
    {
      var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
      return await _httpClient.GetFromJsonAsync<CategoryDetailsDTO>($"api/category/{id}",
        cancellationTokenSource.Token)
          .ConfigureAwait(false);
    }
    catch (HttpRequestException ex)
    {
      // Handle HTTP exceptions such as 404, 500, etc.
      Console.WriteLine($"An error occurred while requesting category {id}: {ex.Message}");
      return null;
    }
    catch (OperationCanceledException)
    {
      // Handle the case when the request is canceled by the token
      Console.WriteLine($"The request for category {id} was canceled due to timeout.");
      return null;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An unexpected error occurred while requesting category {id}: {ex.Message}");
      return null;
    }
  }

  public async Task<CategoryDTO> CreateCategory(CategoryCreateDTO newCategory)
  {
    try{
      var response = await _httpClient.PostAsJsonAsync<CategoryCreateDTO>("api/category", newCategory);
      if (response.IsSuccessStatusCode)
      {
        var responseBody = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<CategoryDTO>(responseBody,
          new JsonSerializerOptions
          {
            PropertyNameCaseInsensitive = true
          });
      }
      return null;
    }  
    catch(Exception ex)
    {
      throw;
    }
  }
}
