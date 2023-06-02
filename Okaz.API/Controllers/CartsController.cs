using Microsoft.AspNetCore.Mvc;
using Okaz.Models;
using Okaz.Okaz.API.Models.Repositories;

namespace Okaz.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
  private readonly ICartRepository _repository;

  public CartsController(ICartRepository repository)
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