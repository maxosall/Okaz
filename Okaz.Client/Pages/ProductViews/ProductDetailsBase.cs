using Microsoft.AspNetCore.Components;
using Okaz.Models;
using Okaz.Client.Services;
using Okaz.API.Models.Repositories;
using Okaz.API.Models.DTOs;

namespace Okaz.Client.Pages.ProductViews;

public class ProductDetailsBase: ComponentBase
{
  [Inject]
  public IProductService ProductService { get; set; }  

  [Parameter]
  public string Id {get; set; }

  public ProductDTO Product { get; set; } = new ProductDTO();

  public string ErrorMessage { get;set;}

  protected async override Task OnInitializedAsync()
  {
    try{
      Product = await ProductService.GetProductById(int.Parse(Id));  
    }
    catch(Exception ex) {
      ErrorMessage = ex.Message;
    }
  }
}