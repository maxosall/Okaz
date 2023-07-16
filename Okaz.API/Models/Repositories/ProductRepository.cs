using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Okaz.API.Models;
using Okaz.API.Models.DTOs;
using Okaz.Models;
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


  public async Task<ProductDTO> AddAsync(ProductCreateDTO request)
  {
    if (request == null)
      throw new ArgumentNullException(nameof(request));

    var product = _mapper.Map<Product>(request);

    var result = await _context.Products.AddAsync(product);
    await _context.SaveChangesAsync();

    return _mapper.Map<ProductDTO>(result.Entity);
  }

  public async Task<ProductDTO> DeleteByIdAsync(int id)
  {
    var product = await _context.Products
      .FirstOrDefaultAsync(p => p.ProductId == id);

    if (product != null)
    {
      _context.Products.Remove(product);
      await _context.SaveChangesAsync();
      return _mapper.Map<ProductDTO>(product);
    }
    return null;
  }

  public async Task<IEnumerable<ProductDTO>> GetAll()
  {
    var products = await _context.Products
      .Include(p => p.Category)
      .ToListAsync();

    var results = _mapper.Map<IEnumerable<ProductDTO>>(products);

    return results;
  }

  public async Task<ProductDTO> GetByIdAsync(int id)
  {
    var product = await _context.Products
      .Include(p => p.Category)
      .FirstOrDefaultAsync(x => x.ProductId == id);
    if (product == null) return null;

    return _mapper.Map<ProductDTO>(product);
  }

  public async Task<ProductDTO> Update(ProductCreateDTO dto)
  {
    if (dto == null) throw new ArgumentNullException(nameof(dto));

    var product = await _context.Products.FindAsync(dto.ProductId);
    if (product == null) return null;

    _mapper.Map(dto, product);
    _context.Products.Update(product);
    await _context.SaveChangesAsync();
    
    return _mapper.Map<ProductDTO>(product);
  }

  // public async Task<Product> Update(ProductCreateDTO dto)
  // {
  //   if (dto == null) throw new ArgumentNullException(nameof(dto));

  //   var product = await _context.Products.FindAsync(dto.ProductId);
  //   if (product == null) return null;

  //   _mapper.Map(dto, product);

  //   await _context.SaveChangesAsync();
  //   return product;
  // }
}