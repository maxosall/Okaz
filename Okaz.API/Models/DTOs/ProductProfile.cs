using AutoMapper;
using Okaz.Models;
using Okaz.API.Models.DTOs;

namespace Okaz.API.Models.DTOs;

public class ProductProfile : Profile
{
  public ProductProfile()
  {
    CreateMap<Product, ProductCreateDTO>();
    CreateMap<ProductCreateDTO, Product>();

    CreateMap<Product, ProductDTO>()
      .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
      .ReverseMap(); 

  }
}