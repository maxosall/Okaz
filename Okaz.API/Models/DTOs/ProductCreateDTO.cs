using System.ComponentModel.DataAnnotations;
using Okaz.Models;
namespace Okaz.Okaz.API.Models.DTOs;

public class ProductCreateDTO
{
  public int ProductId { get; set;}
  [Required]
  [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
  public string Name { get; set; }
  [Required]
  [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
  public string Description { get; set; }

  [Required]
  [Range(0, double.MaxValue)]
  public decimal Price { get; set; }

  [Url]
  public string ImageUrl { get; set; }

  public int? CategoryId { get; set; }

  // public virtual Category Category { get; set; }

}