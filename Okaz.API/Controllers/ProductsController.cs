using Microsoft.AspNetCore.Mvc;
using Okaz.API.Models.Repositories;
using Okaz.Models;
using Okaz.API.Models.DTOs;

namespace Okaz.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
      _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), 200)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
      var products = await _repository.GetAll();
      return Ok(products);
    }

    [ProducesResponseType(typeof(ProductDTO), 200)]
    [ProducesResponseType(404)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
      var product = await _repository.GetByIdAsync(id);
      
      if (product == null) return NotFound();
      
      return Ok(product);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Product), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Product>> CreateProduct(ProductCreateDTO request)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      try
      {
        var product = await _repository.AddAsync(request);
        return CreatedAtAction(
            nameof(GetProduct), new { id = product.ProductId }, product);
      }
      catch (Exception ex)
      {
        // _logger.LogError(ex, "An error occurred while creating a category");
        return StatusCode(500, "An error occurred while creating a category");
      }
    }

    [HttpPut()]
    public async Task<IActionResult> UpdateProduct(ProductCreateDTO product)
    {
      try
      {
        var productToUpdate = await _repository.GetByIdAsync(product.ProductId);
        if (productToUpdate == null)
        {
          return NotFound($"No product with {product.ProductId} was found");
        }
        var updatedProduct = await _repository.Update(product);
        return Ok(updatedProduct);
      }
      catch(Exception ex){
        throw;
      }
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
