using Microsoft.AspNetCore.Mvc;
using Okaz.API.Models.DTOs;
using Okaz.API.Models.Interfaces;
using Okaz.API.Models.Repositories;
using Okaz.Models;

namespace Okaz.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly IUnitOfWork _uow;
    public ProductsController(IUnitOfWork uow)
    {
      _uow = uow ?? throw new ArgumentNullException(nameof(uow));
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), 200)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
      try
      {
        var products = await _uow.ProductRepository.GetAll();
        return Ok(products);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An error occurred while RETRIEVING Products: {ex.Message} ");
      }
    }

    [ProducesResponseType(typeof(ProductDTO), 200)]
    [ProducesResponseType(404)]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
      try
      {
        var product = await _uow.ProductRepository.GetByIdAsync(id);

        if (product == null) return NotFound();

        return Ok(product);
      }
      catch (Exception ex)
      {
        // _logger.LogError(ex, "An error occurred while creating a category");
        return StatusCode(500, $"An error occurred while RETRIEVING a Product: {ex.Message} ");
      }
    }

    // [HttpPost]
    // [ProducesResponseType(typeof(ProductDTO), 201)]
    // [ProducesResponseType(400)]
    // public async Task<ActionResult<ProductDTO>> CreateProduct(ProductCreateDTO request)
    // {
    //   if (request is null) return BadRequest("No Product Provided");
    //   try
    //   {
    //     var product = await _uow.ProductRepository.AddAsync(request);
    //     return CreatedAtAction(
    //         nameof(GetProduct), new { id = product.ProductId }, product);
    //   }
    //   catch (Exception ex)
    //   {
    //     // _logger.LogError(ex, "An error occurred while creating a category");
    //     return StatusCode(500, $"An error occurred while creating a Product: {ex.Message} ");
    //   }
    // }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDTO), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ProductDTO>> CreateProduct(ProductCreateDTO request)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      try
      {
        var product = await _uow.ProductRepository.AddAsync(request);
        return CreatedAtAction(
            nameof(GetProduct), new { id = product.ProductId }, product);
      }
      catch (Exception ex)
      {
        // _logger.LogError(ex, "An error occurred while creating a category");
        return StatusCode(500, $"An error occurred while CREATING a Product: {ex.Message} ");
      }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductCreateDTO product)
    {
      try
      {
        var productToUpdate = await _uow.ProductRepository.GetByIdAsync(product.ProductId);
        if (productToUpdate == null)
        {
          return NotFound($"No product with {product.ProductId} was found");
        }
        var updatedProduct = await _uow.ProductRepository.Update(product);
        return Ok(updatedProduct);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An error occurred while DELETING a Product: {ex.Message} ");
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductDTO>> DeleteProduct(int id)
    {
      var product = await _uow.ProductRepository.GetByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      await _uow.ProductRepository.DeleteByIdAsync(id);
      return product;
    }
  }
}
