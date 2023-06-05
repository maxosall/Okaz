using Microsoft.EntityFrameworkCore;
using Okaz.API.Models;
using Okaz.Models;

namespace Okaz.API.Models.Repositories
{
  public class CartRepository : ICartRepository
  {
    private readonly OkazDbContext _context;

    public CartRepository(OkazDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Cart>> GetAll()
    {
      return await _context.Carts.ToListAsync();
    }
  }
}