using Microsoft.EntityFrameworkCore;
using Okaz.Models;
using Okaz.API.Models.DTOs;

namespace Okaz.API.Models.Interfaces;


public interface ICategoryRepository
{
  Task<IEnumerable<CategoryDTO>> GetAll();

  Task<CategoryDetailsDTO> GetByIdAsync(int id);

  Task<bool> CheckForCategory(string categoryName);

  Task<CategoryDTO> AddAsync(CategoryCreateDTO entity);

  Task<CategoryDTO> Update(CategoryCreateDTO entity);

  Task<bool> DeleteByIdAsync(int id);
}
