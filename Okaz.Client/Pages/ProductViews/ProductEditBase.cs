using Microsoft.AspNetCore.Components;
using Okaz.API.Models.DTOs;
using Okaz.Client.Services;

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
  public ProductDTO Product { get; set; } = new ();

  public List<CategoryDTO> CategoryList { get; set; } = new();

  public string? ErrorMessage { get; set; }
  public string CategoryId { get; set; }
  public string PageHeating {get; set;}
  public string SubmitButtonText {get; set;}
  protected async Task HandleSubmit()
  {
    var productUpdateDto = new ProductCreateDTO
    {
      ProductId = Product.ProductId,
      Name = Product.Name,
      Description = Product.Description,
      Price = Product.Price,
      ImageUrl = Product.ImageUrl,
      CategoryId = Product.CategoryId
      // CategoryId = int.Parse(CategoryId)
    };
    var result = await ProductService.UpdateProduct(productUpdateDto);

    if (result != null)
    {
      NavigationManager.NavigateTo("/");
    }
  }
  protected override async Task OnInitializedAsync()
  {
    try
    {
      Product = await ProductService.GetProductById(int.Parse(Id));
      CategoryList = (await CategoryService.GetCategories()).ToList();
      CategoryId = Product.CategoryId.ToString();

      if(NavigationManager.Uri == "/product-edit/{id}")
      {
        PageHeating = "Edit Product";     
        SubmitButtonText = "Save Changes";
      }
      else if(NavigationManager.Uri == "/product-create")
      {
        PageHeating ="Create a New Product";
        SubmitButtonText = "Submit";
      }
			ErrorMessage = null;
    }
    catch (Exception ex)
    {
      ErrorMessage = ex.Message;
    }
  }
}