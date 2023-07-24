using Microsoft.AspNetCore.Mvc;
using Okaz.Models;
using Okaz.API.Models.Repositories;

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
  public async Task<ActionResult<IEnumerable<Cart>>> GetCart()
  {
    return Ok(await _repository.GetAll());
  }
}