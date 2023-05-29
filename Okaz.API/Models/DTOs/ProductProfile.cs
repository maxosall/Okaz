using AutoMapper;
using Okaz.Models;
using Okaz.Okaz.API.Models.DTOs;

namespace Okaz.API.Models.DTOs;

public class ProductProfile : Profile
{
  public ProductProfile()
  {
    CreateMap<Product, ProductCreateDTO>();
    CreateMap<ProductCreateDTO, Product>();
  }
}