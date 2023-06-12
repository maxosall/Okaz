using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Okaz.API.Models.DTOs;
using Okaz.API.Models.DTOs;

namespace Okaz.API.Extensions;

public static class WebHostBuilderExtension
{
  public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
  {
    // Register AutoMapper once and pass all the profile types as an array
    services.AddAutoMapper(
      profileAssemblyMarkerTypes: new[] 
      { 
        typeof(CategoryProfile), 
        typeof(ProductProfile) 
      });
    return services;
  }
}

