using Microsoft.AspNetCore.Components;
using Okaz.API.Models.DTOs;
using Okaz.Client.Services;

namespace Okaz.Client.Pages.CategoryViews;
public partial class CategoryDetails : ComponentBase
{
  [Inject]
  public ICategoryService categoryService { get; set; }

  public CategoryDetailsDTO Category { get; set; }
  [Parameter]
  public string Id { get; set; }

  public string ErrorMessage { get; set; } = string.Empty;
  public int ProductCount { get; set; } = 0;
  protected override async Task OnInitializedAsync()
  {
    try
    {
      Category = await categoryService.GetCategoyById(int.Parse(Id));
      ProductCount = Category.Products.Count;
      ErrorMessage = string.Empty;
    }
    catch (Exception ex)
    {
      ErrorMessage = ex.Message;
    }
  }
}