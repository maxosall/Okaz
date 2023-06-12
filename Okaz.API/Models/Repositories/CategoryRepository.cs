using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Okaz.Models;
using Okaz.API.Models.DTOs;

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

  public async Task<IEnumerable<Category>> GetAll()
  {
    return await _context.Categories.Include(c => c.Products)
    .ToListAsync();
  }

  public async Task<Category> GetByIdAsync(int id)
  {
    return await _context.Categories
      .Include(c => c.Products)
      .FirstOrDefaultAsync(x => x.CategoryId == id);
  }
  // public async Task<CategoryDetailsDTO> GetByIdAsync(int id)
  // {
  //   var category = await _context.Categories
  //     .Include(c => c.Products)
  //     .FirstOrDefaultAsync(c => c.CategoryId == id);

  //   var categoryDetailsDTO = _mapper.Map<CategoryDetailsDTO>(category);
  //   return categoryDetailsDTO;
  // }
  public async Task AddAsync(CategoryCreateDTO dto)
  {
    var category = _mapper.Map<Category>(dto);

    await _context.Categories.AddAsync(category);
    _context.SaveChangesAsync();
  }

  
  public async Task Update(CategoryCreateDTO dto)
  {
    if (dto == null) throw new ArgumentNullException(nameof(dto));

    var category = _mapper.Map<Category>(dto);

    if (category == null)
      throw new InvalidOperationException("Failed to map CategoryCreateDTO to Category");

    _context.Categories.Attach(category);
    _context.Entry(category).State = EntityState.Modified;

    await _context.SaveChangesAsync();
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
