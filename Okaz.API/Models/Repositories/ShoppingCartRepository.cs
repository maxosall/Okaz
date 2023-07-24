using Microsoft.EntityFrameworkCore;
using Okaz.API.Models;
using Okaz.API.Models.DTOs;
using Okaz.Models;
using AutoMapper;

namespace Okaz.API.Models.Repositories
{
  public class ShoppingCartRepository : IShoppingCartRepository
  {
    private readonly OkazDbContext _context;
    private readonly IMapper _mapper;

    public ShoppingCartRepository(OkazDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    public async Task<IEnumerable<Cart>> GetAll()
    {
      return await _context.Carts.ToListAsync();
    }

    // public async Task<CartItemDto> GetItem(int id)
    // {
    //   var product = _context.Products.Where(i => i.ProductId == )
    // }
    private async Task<bool> CartItemExisits(int cartId, int productId)
    {
      return await _context.CartItems
        .AnyAsync(x => x.CartId == cartId && x.ProductId == productId);
    }

    public async Task<CartItemDto> AddItem(CartItemCreateDto cartItemToAddDTO)
    {
      if (cartItemToAddDTO == null)
        throw new ArgumentNullException(nameof(cartItemToAddDTO));
      if (await CartItemExisits(cartItemToAddDTO.CartId, cartItemToAddDTO.ProductId) == false)
      {
        var item = await _context.Products
          .FirstOrDefaultAsync(i => i.ProductId == cartItemToAddDTO.ProductId);

        var product = _mapper.Map<CartItem>(cartItemToAddDTO);

        if (item != null){
          var result = await _context.CartItems.AddAsync(product);
          await _context.SaveChangesAsync();
          return _mapper.Map<CartItemDto>(result.Entity);
        }
      }
      return null;
    }
  }
}