namespace Okaz.API.Models.DTOs;

public record CartItemDto(
  int CartItemId,
  int ProductId,
  int CartId,
  string ProductName,
  string ProductDescription,
  string ProductImageURL,
  decimal Price,
  decimal TotalPrice,
  int Quantiy );