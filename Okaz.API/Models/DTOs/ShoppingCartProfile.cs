using Okaz.Models;
using Okaz.API.Models.DTOs;
using AutoMapper;

namespace Okaz.API.Models.DTOs;

public class ShoppingCartProfile: Profile
{
  public ShoppingCartProfile()
  {
    CreateMap<CartItem, CartItemDto>()
      .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
      .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product.Description))
      .ForMember(dest => dest.ProductImageURL, opt => opt.MapFrom(src => src.Product.ImageUrl))
      .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
      .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src=> src.Product.Price * src.Quantity ));

    CreateMap<CartItem, CartItemCreateDto>();
    CreateMap<CartItemCreateDto, CartItem>();
      // .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.CartId))
      // .ForMember(dest => ProductId, opt => opt.MapFrom(src => src.Product.ProductId))
      // .ForMember(dest => Quantity, opt => opt.MapFrom(src => src.Quantity));

      
  }
}