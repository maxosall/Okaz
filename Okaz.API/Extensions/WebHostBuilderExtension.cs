using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Okaz.API.Models.DTOs;
using Okaz.Okaz.API.Models.DTOs;

namespace Okaz.Okaz.API.Extensions;

public static class WebHostBuilderExtension
{
  public static WebApplicationBuilder AddAutoMapperProfiles(this WebApplicationBuilder builder)
  {
    builder.Services.AddAutoMapper(profileAssemblyMarkerTypes: typeof(CategoryProfile));
    builder.Services.AddAutoMapper(profileAssemblyMarkerTypes: typeof(ProductProfile));
    return builder;
  }
}

