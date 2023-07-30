using AutoMapper;
using Okaz.API.Models;
using Okaz.API.Models.Interfaces;
using Okaz.API.Models.Repositories;
namespace Okaz.Okaz.API.Models;

public class UnitOfWork : IUnitOfWork
{
  private readonly OkazDbContext _context;
  private readonly IMapper _mapper;

  public UnitOfWork(OkazDbContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }
  public UnitOfWork(OkazDbContext context)
  {
    _context = context;
  }

  public IProductRepository ProductRepository
    => new ProductRepository(_context);

  public ICategoryRepository CategoryRepository 
    => new CategoryRepository(_context);

  // public IProductRepository ProductRepository {get; }
  public async Task<bool> SaveAsync()
  {
    return await _context.SaveChangesAsync() > 0;
  }
}