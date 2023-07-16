using Okaz.API.Models.DTOs;
using FluentValidation;

namespace Okaz.API.Models.Validations;
public class CategoryDTOValidator: AbstractValidator<CategoryDTO>
{
  public CategoryDTOValidator()
  {
    RuleFor(dto => dto.Name)
      .NotEmpty()
      .MinimumLength(100);
  }
}