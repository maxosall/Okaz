using Okaz.API.Models.Repositories;
using Okaz.Okaz.API.Models.Repositories;

namespace Okaz.API.Extensions
{
  public static class RepositoryExtensions
  {
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
      _ = services.AddScoped<ICategoryRepository, CategoryRepository>();
      _ = services.AddScoped<IProductRepository, ProductRepository>();
      _ = services.AddScoped<ICartRepository, CartRepository>();
      _ = services.AddScoped<ICartItemRepository, CartItemRepository>();

      return services;
    }
  }
}