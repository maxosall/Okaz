
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Okaz.API.Models.DTOs;
public class CategoryCreateDTO
{
    public int CategoryId { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}
