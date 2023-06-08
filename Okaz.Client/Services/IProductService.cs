
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Okaz.API.Models.DTOs;
using Okaz.Models;

namespace Okaz.Client.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int id);
        // Task<ProductCreateDTO> CreateProduct(ProductCreateDTO reqeust);
        // Task<ProductCreateDTO> UpdateProduct(ProductCreateDTO updatedProduct);
        Task<Product> DeleteProduct(int id);

    }
}