using Okaz.Client.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Okaz.Client.Extensions;

public static class ServivesExtensions
{
  public static IServiceCollection RegisterHttpClientServices(this IServiceCollection services) 
  {
    services.AddScoped(client => 
      new HttpClient {
        BaseAddress = new Uri("https://localhost:7169/")
    });

    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<ICategoryService, CategoryService>();
    // services.AddScoped<ICartService, CartService>();
    // services.AddScoped<ICartItemService, CartItemService>();
    // services.AddScoped<IOrderService, OrderService>();
    // services.AddScoped<IOrderItemService, OrderItemService>();

    return services;
  }   
}