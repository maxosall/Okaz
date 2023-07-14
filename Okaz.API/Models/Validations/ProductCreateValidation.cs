using Okaz.API.Models.DTOs;
using FluentValidation;

namespace Okaz.API.Models.Validations;
public class ProductCreateValidator: AbstractValidator<ProductCreateDTO>
{
  public ProductCreateValidator()
  {
    RuleFor(dto => dto.Name)
      .NotEmpty()
      .MaximumLength(150);

    RuleFor(dto => dto.Description)
      .NotEmpty()
      .MaximumLength(500);

    RuleFor(dto => dto.Price)
      .GreaterThan(0);  

    RuleFor(dto => dto.ImageUrl)
      .NotEmpty()
      .Matches(@"^https?://")
      .When(dto => string.IsNullOrEmpty(dto.ImageUrl) == false);
  }	
    
}