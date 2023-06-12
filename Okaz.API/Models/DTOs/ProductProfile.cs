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

      // AutoMapper configuration
  CreateMap<Category, CategoryDetailsDTO>()
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)) // map category name
    .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products)); // map products list

  CreateMap<Product, ProductDTO>()
    .ForMember(dest => dest.CategoryId, opt => opt.Ignore()) 
    .ForMember(dest => dest.CategoryName, opt => opt.Ignore()) 
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price)) 
    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl)); // map product image url

  }
}