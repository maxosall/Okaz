using Microsoft.EntityFrameworkCore;
using Okaz.API.Models.DTOs;
using Okaz.Models;

namespace Okaz.API.Models.Interfaces;


public interface ICategoryRepository
{
  Task<IEnumerable<Category>> GetAll();

  Task<Category> GetByIdAsync(int id);

  Task<bool> CheckForCategory(string categoryName);

  Task<Category> AddAsync(Category entity);

  Task<Category> Update(Category entity);

  Task<bool> DeleteByIdAsync(int id);
}

