using AutoMapper;
using Okaz.Models;
using Okaz.API.Models.DTOs;

namespace Okaz.API.Models.DTOs;
public class CategoryProfile: Profile
{
	public CategoryProfile()
	{
		CreateMap<Category, CategoryCreateDTO>();
		CreateMap<CategoryCreateDTO, Category>();

	   // CreateMap<Category, CategoryDetailsDTO>()
		 //  .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Products.Name))
		 //  .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src => src.Products.ImageUrl))
		 //  .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Products.Price))
		 //  .ReverseMap(); 

		// // Mapping from Product to CategoryDetailsDTO
    //         CreateMap<Product, CategoryDetailsDTO>()
    //             .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
    //             .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
    //             .ForMember(dest => dest.ProductPrice, opt => opt.MapFrom(src => src.Price))
    //             .ForMember(dest => dest.ProductImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

    //         // Mapping from Category to CategoryDetailsDTO
    //         CreateMap<Category, CategoryDetailsDTO>()
    //             .IncludeMembers(src => src.Products);
	}
}