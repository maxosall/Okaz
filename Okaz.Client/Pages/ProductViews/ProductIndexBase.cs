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
  public int ProductCount { get; set; } 
  protected override async Task OnInitializedAsync()
  {
    Products = (await ProductService.GetProducts()).ToList();
    ProductCount = Products.Count();
  }
}