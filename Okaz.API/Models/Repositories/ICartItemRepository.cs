using Okaz.Models;

namespace Okaz.Okaz.API.Models.Repositories;

public interface ICartItemRepository
{
  Task<IEnumerable<CartItem>> GetAll();
}