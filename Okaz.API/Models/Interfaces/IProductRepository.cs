using Okaz.API.Models.DTOs;
using Okaz.Models;

namespace Okaz.API.Models.Interfaces
{
  public interface IProductRepository
  {
    Task<IEnumerable<Product>> GetAll();

    Task<Product> GetByIdAsync(int id);

    Task<Product> AddAsync(Product entity);

    Task<Product> Update(Product entity);

    Task<Product> DeleteByIdAsync(int id);
  }
}