using Microsoft.EntityFrameworkCore;
using Okaz.Models;
using Okaz.Okaz.API.Models.DTOs;

namespace Okaz.Okaz.API.Models.Repositories;


public interface ICategoryRepository
{
  Task<IEnumerable<Category>> GetAll();

  Task<Category> GetByIdAsync(int id);

  Task AddAsync(CategoryCreateDTO entity);

  Task Update(CategoryCreateDTO entity);

  Task DeleteByIdAsync(int id);
}

