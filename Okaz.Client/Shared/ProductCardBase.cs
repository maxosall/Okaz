using Okaz.Models;
using Okaz.API.Models.DTOs;
using Microsoft.AspNetCore.Components;

namespace Okaz.Client.Shared;

public class ProductCardBase: ComponentBase
{
  [Parameter]
	public ProductDTO Product { get; set; }
}