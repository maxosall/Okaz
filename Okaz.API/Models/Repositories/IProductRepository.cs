using Okaz.API.Models.DTOs;
using Okaz.Models;

namespace Okaz.API.Models.Repositories
{
  public interface IProductRepository
  {
    Task<IEnumerable<ProductDTO>> GetAll();

    Task<ProductDTO> GetByIdAsync(int id);

    Task<ProductDTO> AddAsync(ProductCreateDTO entity);

    Task<ProductDTO> Update(ProductCreateDTO entity);

    Task DeleteByIdAsync(int id);
  }
}