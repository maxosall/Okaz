using Okaz.Models;

namespace Okaz.API.Models.DTOs;

public class CategoryDetailsDTO:CategoryDTO
{
    // public List<ProductDTO> Products { get; set; } 
    public List<ProductSammaryDTO> Products { get; set; } // list of products in the category
}