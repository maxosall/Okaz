using Okaz.API.Models.Interfaces;
using Okaz.API.Models.Repositories;
using Okaz.Okaz.API.Models;

namespace Okaz.API.Extensions
{
  public static class RepositoryExtensions
  {
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
      _ = services.AddScoped<ICategoryRepository, CategoryRepository>();
      _ = services.AddScoped<IUnitOfWork, UnitOfWork>();
      // _ = services.AddScoped<IProductRepository, ProductRepository>();
      _ = services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
      _ = services.AddScoped<ICartItemRepository, CartItemRepository>();

      return services;
    }
  }
}