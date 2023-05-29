using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Okaz.Models;
using Okaz.Okaz.API.Models.DTOs;
using Okaz.Okaz.API.Models.Repositories;
namespace Okaz.Okaz.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
  private readonly ICategoryRepository _repository;

  public CategoryController(ICategoryRepository repository)
  {
    _repository = repository;
  }

  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<Category>), 200)]
  public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
  {
    var categories = await _repository.GetAll();
    return Ok(categories);
  }

  [HttpGet("{id:int}")]
  [ProducesResponseType(typeof(Category), 200)]
  [ProducesResponseType(404)]
  public async Task<ActionResult<Category>> GetCategoryById(int id)
  {
    var category = await _repository.GetByIdAsync(id);
    if (category == null)
    {
      return NotFound();
    }
    return Ok(category);
  }

  [HttpPost]
  [ProducesResponseType(typeof(Category), 201)]
  [ProducesResponseType(400)]
  public async Task<ActionResult<Category>> CreateCategory([Required] CategoryCreateDTO dto)
  {
    if (!ModelState.IsValid) return BadRequest(ModelState);
    try
    {
      await _repository.AddAsync(dto);
      return CreatedAtAction(
        nameof(GetCategoryById), new { id = dto.CategoryId }, dto);
    }
    catch (Exception ex)
    {
      // _logger.LogError(ex, "An error occurred while creating a category");
      return StatusCode(500, "An error occurred while creating a category");
    }
  }

  [HttpPut("{id:int}")]
  [ProducesResponseType(204)]
  public async Task<IActionResult> UpdateCategory(int id, CategoryCreateDTO dto)
  {
    if (dto.CategoryId != id)
    {
      return BadRequest();
    }
    await _repository.Update(dto);
    return NoContent();
  }

  [HttpDelete("{id:int}")]
  public async Task<IActionResult> DeleteCategory(int id)
  {
    await _repository.DeleteByIdAsync(id);
    return NoContent();
  }
}

