using Okaz.Models;
using Okaz.Okaz.API.Models.DTOs;

namespace Okaz.API.Models.Repositories
{
  public interface IProductRepository
  {
    Task<IEnumerable<ProductDTO>> GetAll();

    Task<ProductDTO> GetByIdAsync(int id);

    Task<Product> AddAsync(ProductCreateDTO entity);

    Task<Product> Update(ProductCreateDTO entity);

    Task DeleteByIdAsync(int id);
  }
}