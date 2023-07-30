using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Okaz.API.Models.DTOs;
using Okaz.API.Models.Interfaces;
using Okaz.Models;

namespace Okaz.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProductsController : ControllerBase
  {
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    public ProductsController(IUnitOfWork uow, IMapper mapper)
    {
      _uow = uow ?? throw new ArgumentNullException(nameof(uow));
      _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDTO>), 200)]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
      try
      {
        var products = await _uow.ProductRepository.GetAll();
        var response = _mapper.Map<IEnumerable<ProductDTO>>(products);
        return Ok(response);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An error occurred while RETRIEVING Products: {ex.Message} ");
      }
    }

    [ProducesResponseType(typeof(ProductDTO), 200)]
    [ProducesResponseType(404)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProduct(int id)
    {
      try
      {

        var product = await _uow.ProductRepository.GetByIdAsync(id);
        var responseDto = _mapper.Map<ProductDTO>(product);

        return responseDto == null ? NotFound() : Ok(responseDto);
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
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {

        // Todo: map ProductCreateDTO -> Product
        var productRequestDto = _mapper.Map<Product>(request);

        var newProduct = await _uow.ProductRepository.AddAsync(productRequestDto);
        await _uow.SaveAsync();

        // Todo: map  Product -> ProductDTO
        var responseDto = _mapper.Map<ProductDTO>(newProduct);

        return CreatedAtAction(
            nameof(GetProduct),
            new { id = responseDto.ProductId },
            responseDto);
      }
      catch (Exception ex)
      {
        // _logger.LogError(ex, "An error occurred while creating a category");
        return StatusCode(500, $"An error occurred while CREATING a Product: {ex.Message} ");
      }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(ProductCreateDTO productDto)
    {
      try
      {
        if (productDto is null)
          return BadRequest($"{nameof(productDto)} is null");

        var productEntity = await _uow.ProductRepository.GetByIdAsync(productDto.ProductId);
        if (productEntity == null)
        {
          return NotFound($"No product with {productDto.ProductId} was found");
        }

        // Todo: Map ProductCreateDTO -> Product
        _mapper.Map(productDto, productEntity);
        var updatedProduct = await _uow.ProductRepository.Update(productEntity);
        await _uow.SaveAsync();

        // Todo: Map Product -> ProductDTO

        var responseDto = _mapper.Map<ProductDTO>(productEntity);
        return Ok(responseDto);
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
      if (product is null)
      {
        return NotFound();
      }
      await _uow.ProductRepository.DeleteByIdAsync(id);
      await _uow.SaveAsync();

      // Todo: Map Produc -> ProductDTO
      return _mapper.Map<ProductDTO>(product);
    }
  }
}
