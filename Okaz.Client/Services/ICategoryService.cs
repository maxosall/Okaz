using Okaz.API.Models.DTOs;

namespace Okaz.Client.Services;
public interface ICategoryService
{
  Task<IEnumerable<CategoryDTO>> GetCategories();
  Task<CategoryDetailsDTO> GetCategoyById(int id);
}