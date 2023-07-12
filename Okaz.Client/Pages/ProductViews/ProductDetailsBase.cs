using Microsoft.AspNetCore.Components;
using Okaz.API.Models.DTOs;
using Okaz.API.Models.Repositories;
using Okaz.Client.Services;
using Okaz.Models;

namespace Okaz.Client.Pages.ProductViews;

public class ProductDetailsBase : ComponentBase
{
  [Inject]
  public IProductService ProductService { get; set; }

  [Parameter]
  public string Id { get; set; }

  public ProductDTO Product { get; set; } = new ProductDTO();

  public string? ErrorMessage { get; set; }

  protected override async Task OnInitializedAsync()
  {
    try
    {
      Product = await ProductService.GetProductById(int.Parse(Id));
      ErrorMessage = null;
    }
    catch (Exception ex)
    {
      ErrorMessage = ex.Message;
    }
  }
}