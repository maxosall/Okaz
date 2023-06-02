using Okaz.Models;

namespace Okaz.Okaz.API.Models.Repositories;

public interface ICartRepository
{
  Task<IEnumerable<Cart>> GetAll();
}