using Okaz.Models;
using Okaz.API.Models.DTOs;
using AutoMapper;

namespace Okaz.API.Models.DTOs;

public class ShoppingCartProfile: Profile
{
  public ShoppingCartProfile()
  {
    CreateMap<CartItem, CartItemDto>()
      .ForMember(des => des.ProductName, otp => otp.MapFrom(src => src.Product.Name))
      .ForMember(des => des.ProductDescription, otp => otp.MapFrom(src => src.Product.Description))
      .ForMember(des => des.ProductImageURL, otp => otp.MapFrom(src => src.Product.ImageUrl))
      .ForMember(des => des.Price, otp => otp.MapFrom(src => src.Product.Price));

    CreateMap<CartItem, CartItemCreateDto>();
  }
}