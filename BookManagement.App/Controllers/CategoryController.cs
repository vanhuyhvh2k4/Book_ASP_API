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
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCategory([FromRoute] int categoryId)
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
        }

        [HttpGet("book/{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCategoriesOfBook([FromRoute] int bookId)
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
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateCategory([FromBody] CategoryDto createCategory)
        {
            if (createCategory == null)
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

            if(!_categoryRepository.CreateCategory(newCategory))
            {
                ModelState.AddModelError("", "Something went wrong while creating");
                return StatusCode(500, ModelState);
            }

            return Ok("Created successfully");
        }
    }
}
