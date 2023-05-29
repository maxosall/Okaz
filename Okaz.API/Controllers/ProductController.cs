using Microsoft.AspNetCore.Mvc;
using Okaz.API.Models.Repositories;
using Okaz.Models;
using Okaz.Okaz.API.Models.DTOs;

namespace Okaz.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductController : ControllerBase
  {
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
      this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), 200)]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
      var products = await _repository.GetAll();
      return Ok(products);
    }

    [ProducesResponseType(typeof(Product), 200)]
    [ProducesResponseType(404)]
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
      var product = await _repository.GetByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Product>> CreateProduct(ProductCreateDTO dto)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      try
      {
        var product = await _repository.AddAsync(dto);
        return CreatedAtAction(
            nameof(GetProduct), new { id = product.ProductId }, product);
      }
      catch (Exception ex)
      {
        // _logger.LogError(ex, "An error occurred while creating a category");
        return StatusCode(500, "An error occurred while creating a category");
      }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, ProductCreateDTO dto)
    {
      if (id != dto.ProductId)
      {
        return BadRequest();
      }
      var product = await _repository.Update(dto);
      if (product == null)
      {
        return NotFound();
      }
      return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
      var product = await _repository.GetByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      await _repository.DeleteByIdAsync(id);
      return NoContent();
    }
  }
}
