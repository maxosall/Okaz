using Microsoft.AspNetCore.Components;
using Okaz.API.Models.DTOs;
using Okaz.Client.Services;

namespace Okaz.Client.Pages.CategoryViews;
public partial class CategoryIndex : ComponentBase
{
  [Inject]
  public ICategoryService CategoryService { get; set; }

  public List<CategoryDTO> Categories { get; set; } = new();

  protected override async Task OnInitializedAsync()
  {
    try
    {
      Categories = (await CategoryService.GetCategories()).ToList();
    }
    catch (Exception ex)
    {
      throw;
    }
  }
}