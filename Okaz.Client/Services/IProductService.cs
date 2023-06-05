
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Okaz.API.Models.DTOs;

namespace Okaz.Client.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
    }
}