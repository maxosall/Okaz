using Microsoft.AspNetCore.Mvc;
using Okaz.Models;
using Okaz.API.Models.Repositories;
using Okaz.API.Models.DTOs;
using Okaz.API.Models.Interfaces;

namespace Okaz.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartsController : ControllerBase
{
  private readonly IShoppingCartRepository _repository;

  public ShoppingCartsController(IShoppingCartRepository repository)
  {
    _repository = repository ??
        throw new ArgumentNullException(nameof(repository));
  }

  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<Cart>), 200)]
  // public async Task<ActionResult<IEnumerable<Cart>>> GetItems()
  // {
  //   return Ok(await _repository.GetAll());
  // }

  [ProducesResponseType(typeof(CartItemDto), 200)]
  [ProducesResponseType(404)]
  [HttpGet("{id:int}")]
  public async Task<ActionResult<CartItemDto>> GetItem(int id)
  {
    try{
      var cartItem = await _repository.GetItem(id);

      if (cartItem == null) return NotFound();

      return Ok(cartItem);
    }
    catch (Exception ex)
    {
      // _logger.LogError(ex, "An error occurred while creating a cartItem");
      return StatusCode(500, $"An error occurred while RETRIEVING a CartItem: {ex.Message} ");
    }
  }

  [HttpPost]
  [ProducesResponseType(typeof(CartItemDto), 201)]
  [ProducesResponseType(400)]
  public async Task<ActionResult<CartItemDto>> AddItem([FromBody] CartItemCreateDto request)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      var newCartItem = await _repository.AddItem(request);
      if (newCartItem is null) return NoContent();
      return CreatedAtAction(
        nameof(GetItem), new { id = newCartItem.ProductId }, newCartItem);
    }
    catch (Exception ex)
    {
      // _logger.LogError(ex, "An error occurred while creating a CartItem");
      return StatusCode(500, $"An error occurred while CREATING a CartItem: {ex.Message} ");
    }
  }
}