using Okaz.Models;

namespace Okaz.API.Models.Repositories;

public interface ICartRepository
{
  Task<IEnumerable<Cart>> GetAll();
}