using Okaz.Models;

namespace Okaz.API.Models.Interfaces;

public interface ICartItemRepository
{
  Task<IEnumerable<CartItem>> GetAll();
}