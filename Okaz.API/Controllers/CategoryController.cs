using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Okaz.API.Models.DTOs;
using Okaz.API.Models.Repositories;
using Okaz.Models;

namespace Okaz.API.Controllers;

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
  [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), 200)]
  public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
  {
    var categories = await _repository.GetAll();
    return Ok(categories);
  }

  [HttpGet("{id:int}")]
  [ProducesResponseType(typeof(CategoryDetailsDTO), 200)]
  [ProducesResponseType(404)]
  public async Task<ActionResult<CategoryDetailsDTO>> GetCategoryById(int id)
  {
    try
    {
      var category = await _repository.GetByIdAsync(id);
      if (category == null)
      {
        return NotFound($"Category with id {id} not found.");
      }
      return Ok(category);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Something went wrong: {ex.Message} ");
    }
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

