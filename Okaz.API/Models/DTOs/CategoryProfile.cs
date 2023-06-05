using AutoMapper;
using Okaz.Models;

namespace Okaz.API.Models.DTOs;
public class CategoryProfile: Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryCreateDTO>();
        CreateMap<CategoryCreateDTO, Category>();
    }
}