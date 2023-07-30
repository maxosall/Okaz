using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Okaz.API.Models;
using Okaz.API.Models.DTOs;
using Okaz.API.Models.Interfaces;
using Okaz.Models;

namespace Okaz.API.Models.Repositories;

public class ProductRepository : IProductRepository
{
  private readonly OkazDbContext _context;

  public ProductRepository(OkazDbContext context)
  {
    _context = context;
  }


  public async Task<Product> AddAsync(Product product)
  {
    if (product == null)
      throw new ArgumentNullException(nameof(product));

    var result = await _context.Products.AddAsync(product);
    return result.Entity;
  }

  public async Task<Product> DeleteByIdAsync(int id)
  {
    var product = await _context.Products
      .FirstOrDefaultAsync(p => p.ProductId == id);

    if (product is null) return null;

    _context.Products.Remove(product);

    return product;
  }

  public async Task<IEnumerable<Product>> GetAll()
  {
    var products = await _context.Products
      .Include(p => p.Category)
      .ToListAsync();

    return products;
  }

  public async Task<Product> GetByIdAsync(int id)
  {
    var product = await _context.Products
      .Include(p => p.Category)
      .FirstOrDefaultAsync(x => x.ProductId == id);
    if (product == null) return null;

    return product;
  }

  public async Task<Product> Update(Product product)
  {
    if (product == null) throw new ArgumentNullException(nameof(product));

    var productToUpdate = await _context.Products.FindAsync(product.ProductId);
    if (productToUpdate == null) return null;

    _context.Products.Update(productToUpdate);
    return productToUpdate;
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