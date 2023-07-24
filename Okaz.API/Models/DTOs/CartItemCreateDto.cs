namespace Okaz.API.Models.DTOs;

public record CartItemCreateDto(
  int CartId,
  int ProductId,
  int Quantity);