using Microsoft.EntityFrameworkCore;
using Okaz.Models;
using Okaz.API.Models.DTOs;

namespace Okaz.API.Models.Repositories;


public interface ICategoryRepository
{
  Task<IEnumerable<CategoryDTO>> GetAll();

  Task<CategoryDetailsDTO> GetByIdAsync(int id);

  Task AddAsync(CategoryCreateDTO entity);

  Task Update(CategoryCreateDTO entity);

  Task DeleteByIdAsync(int id);
}

