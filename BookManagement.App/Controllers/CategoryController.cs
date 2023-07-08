using AutoMapper;
using BookManagement.App.Dto;
using BookManagement.App.Interfaces;
using BookManagement.App.Models;
using BookManagement.App.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.App.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCategories()
        {
            try
            {
                var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (categories == null)
                {
                    return NotFound("Not Found Category");
                }

                return Ok(categories);
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while getting",
                    Message = ex.Message,
                });
            }
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCategory([FromRoute] int categoryId)
        {
            try
            {
                var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (category == null)
                {
                    return NotFound("Not Found Category");
                }

                return Ok(category);
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while getting",
                    Message = ex.Message,
                });
            }
        }

        [HttpGet("book/{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCategoriesOfBook([FromRoute] int bookId)
        {
            try
            {
                var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategoriesOfBook(bookId));

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (categories.Count == 0)
                {
                    return NotFound("Not Found Category");
                }

                return Ok(categories);
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while getting",
                    Message = ex.Message,
                });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CategoryDto createCategory)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var isExist = _categoryRepository
                                .GetCategories()
                                .Where(c => c.Name.Trim().ToUpper() == createCategory.Name.Trim().ToUpper())
                                .FirstOrDefault();

                if (isExist != null)
                {
                    ModelState.AddModelError("", "Category already exists");
                    return StatusCode(409, ModelState);
                }

                var newCategory = new Category()
                {
                    Name = createCategory.Name.Trim()
                };

                _categoryRepository.CreateCategory(newCategory);

                return Ok("Created successfully");
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while creating",
                    Message = ex.Message,
                });
            }
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateCategory([FromRoute] int categoryId, [FromBody] CategoryDto updateCategory)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (categoryId != updateCategory.Id)
                {
                    return BadRequest("Id is not match");
                }

                var categoryMap = _mapper.Map<Category>(updateCategory);

                _categoryRepository.UpdateCategory(categoryMap);

                return Ok("Updated successfully");
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while updating",
                    Message = ex.Message,
                });
            }
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory([FromRoute] int categoryId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var categoryToDelete = _categoryRepository.GetCategory(categoryId);

                if (categoryToDelete == null)
                {
                    return NotFound("Not Found Category");
                }

                _categoryRepository.DeleteCategory(categoryToDelete);

                return Ok("Deleted successfully");
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while deleting",
                    Message = ex.Message,
                });
            }
        }
    }
}
