using Microsoft.EntityFrameworkCore;
using Okaz.Models;
using Okaz.API.Models;
using Okaz.API.Models.Repositories;

namespace Okaz.API.Models.Repositories
{
  public class CartItemRepository : ICartItemRepository
  {
    private readonly OkazDbContext _context;

    public CartItemRepository(OkazDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<CartItem>> GetAll()
    {
      return await _context.CartItems.ToListAsync();
    }
  }
}