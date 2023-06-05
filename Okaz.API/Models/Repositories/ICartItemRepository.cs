using Okaz.Models;

namespace Okaz.API.Models.Repositories;

public interface ICartItemRepository
{
  Task<IEnumerable<CartItem>> GetAll();
}