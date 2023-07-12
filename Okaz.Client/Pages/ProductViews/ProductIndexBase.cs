using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Okaz.API.Models.DTOs;
using Okaz.Client.Services;

namespace Okaz.Client.Pages.ProductViews;

public class ProductIndexBase : ComponentBase
{
  [Inject]
  public IProductService ProductService { get; set; }
  public List<ProductDTO> Products { get; set; }
  public int ProductCount { get; set; } = 0;
  public string? ErrorMessage { get; set; }
  protected override async Task OnInitializedAsync()
  {
    try
    {
      Products = (await ProductService.GetProducts()).ToList();
      ProductCount = Products.Count;
      ErrorMessage = null;
    }
    catch (Exception ex)
    {
      ErrorMessage = ex.Message;
    }

  }
}