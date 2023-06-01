using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Okaz.Models;
using Okaz.Okaz.API.Models;
using Okaz.Okaz.API.Models.DTOs;
// using System.Threading.Task;

namespace Okaz.API.Models.Repositories;

public class ProductRepository : IProductRepository
{
  private readonly OkazDbContext _context;
  private readonly IMapper _mapper;

  public ProductRepository(OkazDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }


  public async Task<Product> AddAsync(ProductCreateDTO request)
  {
    if (request == null)
      throw new ArgumentNullException(nameof(request));
    
    var product = _mapper.Map<Product>(request);

    var result = await _context.Products.AddAsync(product);
    await _context.SaveChangesAsync();

    return result.Entity;
  }

  public async Task DeleteByIdAsync(int id)
  {
    var product = await _context.Products
      .FirstOrDefaultAsync(p => p.ProductId == id);

    if (product != null)
    {
      _context.Products.Remove(product);
      await _context.SaveChangesAsync();
    }
    // return Task.CompletedTask;  
  }

  public async Task<IEnumerable<ProductDTO>> GetAll()
  {
    var products = await _context.Products
      .Include(p=> p.Category)
      .ToListAsync();

    var results= _mapper.Map<IEnumerable<ProductDTO>>(products); 

    return results;  
  }

  public async Task<ProductDTO> GetByIdAsync(int id)
  {
    var product =  await _context.Products
      .Include(p => p.Category)
      .FirstOrDefaultAsync(x => x.ProductId == id);
    if(product == null ) return null;

    return _mapper.Map<ProductDTO>(product);
  }

  public async Task<Product> Update(ProductCreateDTO dto)
  {
    if (dto == null) throw new ArgumentNullException(nameof(dto));

    var product = await _context.Products.FindAsync(dto.ProductId);
    if (product == null) return null;

    _mapper.Map(dto, product);

    await _context.SaveChangesAsync();
    return product;
  }
}