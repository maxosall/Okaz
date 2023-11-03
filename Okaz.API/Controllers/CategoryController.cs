using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Okaz.API.Models.DTOs;
using Okaz.API.Models.Interfaces;
using Okaz.API.Models.Repositories;
using Okaz.Models;


namespace Okaz.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class CategoriesController : ControllerBase
  {
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    public CategoriesController(IUnitOfWork uow, IMapper mapper)
    {
      _uow = uow;
      _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoryDTO>), 200)]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
      var categories = await _uow.CategoryRepository.GetAll();
      var response = _mapper.Map<IEnumerable<CategoryDTO>>(categories);

      return Ok(response);
    }


    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CategoryDetailsDTO), 200)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(int id)
    {
      try
      {
        var category = await _uow.CategoryRepository.GetByIdAsync(id);
        var categoryDetailsDTO = _mapper.Map<CategoryDetailsDTO>(category);

        return categoryDetailsDTO == null
        ? NotFound($"Category with id: ({id}) not found.")
        : Ok(categoryDetailsDTO);
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
    //     CategoryDetailsDTO category = await _uow.CategoryRepository.GetByIdAsync(id);
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
    public async Task<IActionResult> CreateCategory(CategoryCreateDTO newCategory)
    {
      try
      {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (newCategory == null)
          return BadRequest($"{nameof(CategoryCreateDTO)} is null");

        bool categoryExists = await _uow.CategoryRepository.CheckForCategory(newCategory.Name);
        if (categoryExists)
          return BadRequest($"({newCategory.Name}) category name already exists");


        // Todo: Map CategoryCreateDTO -> Category  
        var categoryEntity = _mapper.Map<Category>(newCategory);
        var category = await _uow.CategoryRepository.AddAsync(categoryEntity);
        await _uow.SaveAsync();

        // Todo: Map Category -> CategoryDTO
        var responseDto = _mapper.Map<CategoryDTO>(category);
        return CreatedAtAction(
          nameof(GetCategoryById),
          new { id = responseDto.CategoryId },
          responseDto);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An Error occurred while creating a category, {ex.Message} ");
      }
    }

    [HttpPut]
    [ProducesResponseType(typeof(CategoryDTO), 204)]
    public async Task<IActionResult> UpdateCategory(CategoryCreateDTO requestDto)
    {
      try
      {
        if (requestDto is null)
          return BadRequest($"{nameof(requestDto)} is null");

        var categoryEntity = await _uow.CategoryRepository.GetByIdAsync(requestDto.CategoryId);
        if (categoryEntity is null)
        {
          return BadRequest($"No category with ID: ({requestDto.CategoryId}) was found");
        }

        // if this category Name Already exists      
        bool categoryExists = await _uow.CategoryRepository
          .CheckForCategory(requestDto.Name);
        if (categoryExists )
        {
          return BadRequest($"({requestDto.Name}) category name already exists");
        }

        // if this category Name exists, Perform (Update)
        // 
        _mapper.Map(requestDto, categoryEntity);
        var result = await _uow.CategoryRepository.Update(categoryEntity);
        var responseDto = _mapper.Map<CategoryDTO>(categoryEntity);
        return Ok(responseDto);
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
        var deleted = await _uow.CategoryRepository.DeleteByIdAsync(id);
        if (deleted)
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