using Microsoft.AspNetCore.Components;
using Okaz.API.Models.DTOs;
using Okaz.Client.Services;
using Okaz.Client.Shared;

namespace Okaz.Client.Pages.ProductViews;

public class ProductEditBase : ComponentBase
{
  [Parameter]
  public string Id { get; set; }
  [Inject]
  public IProductService ProductService { get; set; }
  [Inject]
  public ICategoryService CategoryService { get; set; }
  [Inject]
  public NavigationManager NavigationManager { get; set; }
  public ProductDTO Product { get; set; } = new();

  public List<CategoryDTO> CategoryList { get; set; } = new();

  public ConfirmationModel DeleteConfirmation{get;set;}
  public string? ErrorMessage { get; set; }
  public string CategoryId { get; set; }
  protected async Task HandleSubmit()
  {
    var productRequest = new ProductCreateDTO
    {
      ProductId = Product.ProductId,
      Name = Product.Name,
      Description = Product.Description,
      Price = Product.Price,
      ImageUrl = Product.ImageUrl,
      CategoryId = Product.CategoryId
      // CategoryId = int.Parse(CategoryId)
    };

    ProductDTO result= null;
    if(Product.ProductId != 0)
    {
      result =await ProductService.UpdateProduct(productRequest);
    }
    else
    { 
      result= await ProductService.CreateProduct(productRequest);
    }

    if (result != null) NavigationManager.NavigateTo("/");

  }
  // protected async Task HandleDeleteProduct()
  // {
  //   ProductDTO result = null;
  //   if(Product.ProductId != 0)
  //     result = await ProductService.DeleteProduct(Product.ProductId);
    
  //    if (result is not null) NavigationManager.NavigateTo("/");

  // }

  protected void HandleDeleteProduct()
  {
    DeleteConfirmation.Show();
    StateHasChanged();
  }

  protected async Task HandleConfirmDelete(bool deleteConfirmed)
  {
    if (deleteConfirmed)
    {
      ProductDTO result = null;

      if(Product.ProductId != 0)
        result = await ProductService.DeleteProduct(Product.ProductId);

      if (result is not null) NavigationManager.NavigateTo("/");
    }
  }

  protected override async Task OnInitializedAsync()
  {
    try
    {
      int.TryParse(Id, out int productId);
      if (productId != 0)
      {
        Product = await ProductService.GetProductById(int.Parse(Id));
      }
      else{
        Product.CategoryId =1;
      }
      
      CategoryList = (await CategoryService.GetCategories()).ToList();      

      ErrorMessage = null;
    }
    catch (Exception ex)
    {
      ErrorMessage = ex.Message;
    }
  }
}