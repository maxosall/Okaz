using Microsoft.EntityFrameworkCore;
using Okaz.API.Models;
using Okaz.API.Models.DTOs;
using Okaz.Models;
using AutoMapper;
using Okaz.API.Models.Interfaces;


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

    public async Task<IEnumerable<Cart>> GetAll(int userId)
    {
      return await (from cart in _context.Carts
        join cartItem in _context.CartItems
        on cart.CartId equals cartItem.CartItemId
        where cart.UserId == userId
          select cart)
        .ToListAsync();
      
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

        // Check if the cart item already exists within the users shoping cart
      if (await CartItemExisits(cartItemToAddDTO.CartId, cartItemToAddDTO.ProductId))
        return null;

      // Find the product with the given product id
      var item = await _context.Products
        .FirstOrDefaultAsync(p => p.ProductId == cartItemToAddDTO.ProductId);

      if (item is null)  return null;
          
      var productDto = _mapper.Map<CartItem>(cartItemToAddDTO);  
      var result = await _context.CartItems.AddAsync(productDto);
      await _context.SaveChangesAsync();

      return _mapper.Map<CartItemDto>(result.Entity);         
      
    }

    public async Task<CartItemDto> GetItem(int id)
    {
      // Use a single query to find the cart item by its id
      var cartItem = await _context.CartItems
        .Include(ci => ci.Cart) // Include the related cart entity
        .FirstOrDefaultAsync(ci => ci.CartItemId == id); // Use FirstOrDefaultAsync to get a single result or null

        // Map the cart item entity to a DTO and return it
        return _mapper.Map<CartItemDto>(cartItem);
    }


    // public async Task<CartItemDto> GetItem(int id)
    // {
    //     var cartItem = await _context.CartItems
    //         .Where(c => c.CartItemId == id)
    //         .Select(c => _mapper.Map<CartItemDto>(c))
    //         .FirstOrDefaultAsync();

    //     return cartItem;
    // }

    // public async Task<CartItemDto> GetItem(it id)
    // {

    //   var cartItem =  await (from cart in _context.Carts
    //     join cartItem in _context.CartItems
    //     on cart.CartId equals cartItem.CartItemId)
    //     where cartItem.CartItemId == cart.CartId
    //     select CartItems ; 

    //     return _mapper.Map<CartItemDto>(cartItem); 
    // } 
  }
}