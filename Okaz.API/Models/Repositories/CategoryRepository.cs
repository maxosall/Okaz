using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Okaz.API.Models.DTOs;
using Okaz.API.Models.Interfaces;
using Okaz.Models;

namespace Okaz.API.Models.Repositories;

public class CategoryRepository : ICategoryRepository
{
  private readonly OkazDbContext _context;
  public CategoryRepository(OkazDbContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Category>> GetAll()
  {
    return await _context.Categories.ToListAsync();
  }

  public async Task<bool> CheckForCategory(string categoryName)
  {
    // return await _context.Categories.Select(c => c.Name)
    // .Any(c => string.Equals(c, categoryName, StringComparison.OrdinalIgnoreCase));


    return await _context.Categories
    .AnyAsync(c => c.Name.Trim().ToLower() == categoryName.Trim().ToLower());

  }

  

  // public async Task<Category> GetByIdAsync(int id)
  // {
  //   return await _context.Categories
  //     .Include(c => c.Products)
  //     .FirstOrDefaultAsync(x => x.CategoryId == id);
  // }
  public async Task<Category> GetByIdAsync(int id)
  {
    var category = await _context.Categories
      .Include(c => c.Products)
      .FirstOrDefaultAsync(c => c.CategoryId == id);

    return category;
  }
  public async Task<Category> AddAsync(Category categoryEntity)
  {
    if (categoryEntity == null)
      throw new ArgumentNullException(nameof(categoryEntity));

    var result = await _context.Categories.AddAsync(categoryEntity);
    
    return result.Entity;

  }


  public async Task<Category> Update(Category entity)
  {
    if (entity == null) throw new ArgumentNullException(nameof(entity));

    var existingCategory = await _context.Categories.FindAsync(entity.CategoryId);

    if (existingCategory == null)
      throw new InvalidOperationException("The category to update does not exist");

    var result = _context.Categories.Update(existingCategory);
    return result.Entity;

  }

  public async Task<bool> DeleteByIdAsync(int id)
  {

    var categoryToDelete = await _context.Categories.FindAsync(id);

    if (categoryToDelete == null)
    {
      return false;
    }

    _context.Categories.Remove(categoryToDelete);
    await _context.SaveChangesAsync();
    return true;

  }
}
