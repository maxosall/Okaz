using Okaz.Models;

namespace Okaz.API.Models.DTOs;

public class CategoryDetailsDTO: CategoryDTO
{
	public virtual ICollection<Product>? Products { get; set; }
    public int ProductId { get; set; }

    public string ProductName { get; set; }
  

    public decimal ProductPrice { get; set; }

    public string ProductImageUrl { get; set; }
}
