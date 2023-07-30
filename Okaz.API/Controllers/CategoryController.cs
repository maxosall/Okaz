using Microsoft.AspNetCore.Mvc;
using Okaz.API.Models.DTOs;
using Okaz.API.Models.Repositories;
using Okaz.API.Models.Interfaces;


namespace Okaz.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    private readonly ICategoryRepository _repository;

    public CategoriesController(ICategoryRepository repository)
    {
      _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), 200)]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
      IEnumerable<CategoryDTO> categories = await _repository.GetAll();
      return Ok(categories);
    }


    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CategoryDetailsDTO), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryDetailsDTO>> GetCategoryById(int id)
    {
      try
      {
        CategoryDetailsDTO category = await _repository.GetByIdAsync(id);
        return category == null 
        ? NotFound($"Category with id: ({id}) not found.") 
        : Ok(category);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Something went wrong: {ex.Message} ");
      }
    }


    // [HttpGet("{id:int}")]
    // [ProducesResponseType(typeof(CategoryDetailsDTO), 200)]
    // [ProducesResponseType(404)]
    // public async Task<ActionResult<CategoryDetailsDTO>> GetCategoryById(int id)
    // {
    //   try
    //   {
    //     CategoryDetailsDTO category = await _repository.GetByIdAsync(id);
    //     return category == null 
    //     ? (ActionResult<CategoryDetailsDTO>)NotFound($"Category with id {id} not found.") 
    //     : (ActionResult<CategoryDetailsDTO>)Ok(category);
    //   }
    //   catch (Exception ex)
    //   {
    //     return StatusCode(500, $"Something went wrong: {ex.Message} ");
    //   }
    // }



    [HttpPost]
    [ProducesResponseType(typeof(CategoryDTO), 201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryCreateDTO newCategory)
    {
      try
      {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        if (newCategory == null)
         return BadRequest($"{nameof(CategoryCreateDTO)} is null");
        
        bool categoryExists = await _repository.CheckForCategory(newCategory.Name);
        if (categoryExists)
          return BadRequest($"({newCategory.Name}) category name already exists");
        
        CategoryDTO category = await _repository.AddAsync(newCategory);
        return CreatedAtAction(
          nameof(GetCategoryById), new { id = category.CategoryId }, category);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An Error occurred while creating a category, {ex.Message} ");
      }
    }

    [HttpPut]
    [ProducesResponseType(typeof(CategoryDTO), 204)]
    public async Task<IActionResult> UpdateCategory(CategoryCreateDTO dto)
    {
      try
      {
        if (dto is null)
         return BadRequest($"{nameof(CategoryCreateDTO)} is null");

        var categoryToUpdate = await _repository.GetByIdAsync(dto.CategoryId);
        if (categoryToUpdate is null)
        {
          return BadRequest($"No category with ID: ({dto.CategoryId}) was found");
        }

        // if this category Name Already exists      
        bool categoryExists = await _repository.CheckForCategory(dto.Name);
        if (categoryExists){
          return BadRequest($"({dto.Name}) category name already exists");
        }

        // if this category Name exists, Perform (Update)
        var result =await _repository.Update(dto);
        return Ok(result);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An Error occurred while UPDADING a category, {ex.Message} ");
      }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
      try
      {        
        var deleted = await _repository.DeleteByIdAsync(id);
        if(deleted)
        {
          return Ok($"Category with ID: ({id}) is successfuly deleted.");
        }
        return NotFound($"Category with ID: ({id}) Does Not Exist.");
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An Error occurred while DELETING a category,\n (Error Message)=>: {ex.Message} ");
      }
    }
  }
}