using Okaz.Models;
using Okaz.API.Models.DTOs;
namespace Okaz.API.Models.Repositories;

public interface IShoppingCartRepository
{
  Task<IEnumerable<Cart>> GetAll();

  Task<CartItemDto> AddItem(CartItemCreateDto cartItemToAddDTO);
  // Task<CartItem> UpdateQuantity(int id, cartItemQuantityU)
  // Task<CartItemDto> DeleteItem(it id);
  // Task<CartItemDto> GetItem(it id);
}