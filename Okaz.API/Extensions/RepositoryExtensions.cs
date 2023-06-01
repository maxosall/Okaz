using Okaz.API.Models.Repositories;
using Okaz.Okaz.API.Models.Repositories;

namespace Okaz.Okaz.API.Extensions;

public static class RepositoryExtensions
{
  public static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    services.AddScoped<ICategoryRepository, CategoryRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();
    return services;
  }
}

