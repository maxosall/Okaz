using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Okaz.API.Models.DTOs;
using Okaz.Models;

namespace Okaz.API.Models.Repositories;

public class CategoryRepository : ICategoryRepository
{
  private readonly OkazDbContext _context;
  private readonly IMapper _mapper;
  public CategoryRepository(OkazDbContext context, IMapper mapper)
  {
    _mapper = mapper;
    _context = context;
  }

  public async Task<IEnumerable<CategoryDTO>> GetAll()
  {
    var categories = await _context.Categories
    .ToListAsync();
    var results = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
    return results;
  }

  public async Task<bool> CheckForCategory(string categoryName)
  {
    // return await _context.Categories.Select(c => c.Name)
    // .Any(c => string.Equals(c, categoryName, StringComparison.OrdinalIgnoreCase));


    return await _context.Categories.AnyAsync(c => c.Name.ToLower() == categoryName.ToLower());

  }


  // public async Task<Category> GetByIdAsync(int id)
  // {
  //   return await _context.Categories
  //     .Include(c => c.Products)
  //     .FirstOrDefaultAsync(x => x.CategoryId == id);
  // }
  public async Task<CategoryDetailsDTO> GetByIdAsync(int id)
  {
    var category = await _context.Categories
      .Include(c => c.Products)
      .FirstOrDefaultAsync(c => c.CategoryId == id);

    var categoryDetailsDTO = _mapper.Map<CategoryDetailsDTO>(category);
    return categoryDetailsDTO;
  }
  public async Task<CategoryDTO> AddAsync(CategoryCreateDTO dto)
  {
    if (dto == null)
      throw new ArgumentNullException(nameof(dto));

    var category = _mapper.Map<Category>(dto);

    var result = await _context.Categories.AddAsync(category);
    await _context.SaveChangesAsync();
    return _mapper.Map<CategoryDTO>(result.Entity);

  }


  public async Task<CategoryDTO> Update(CategoryCreateDTO dto)
  {
    if (dto == null) throw new ArgumentNullException(nameof(dto));

    var category = await _context.Categories.FindAsync(dto.CategoryId);

    if (category == null)
      throw new InvalidOperationException("Failed to map CategoryCreateDTO to Category");
      
    _mapper.Map(dto, category);
    _context.Categories.Update(category);
    await _context.SaveChangesAsync();

    return _mapper.Map<CategoryDTO>(category);

  }

  public async Task DeleteByIdAsync(int id)
  {
    var category = await _context.Categories.FindAsync(id);
    if (category != null)
    {
      _context.Categories.Remove(category);
      await _context.SaveChangesAsync();
    }
  }
}
