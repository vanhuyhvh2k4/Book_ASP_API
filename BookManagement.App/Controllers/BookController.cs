using AutoMapper;
using BookManagement.App.Dto;
using BookManagement.App.Interfaces;
using BookManagement.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.App.Controllers
{
    [ApiController]
    [Route("api/book")]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBooks()
        {
            try
            {
                var books = _mapper.Map<List<BookDto>>(_bookRepository.GetBooks());

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (books == null)
                {
                    return NotFound();
                }

                return Ok(books);
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while getting",
                    Message = ex.Message,
                });
            }
        }

        [HttpGet("{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBook([FromRoute] int bookId)
        {
            try
            {
                var book = _mapper.Map<BookDto>(_bookRepository.GetBook(bookId));

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (book == null)
                {
                    return NotFound("Not Found Book");
                }

                return Ok(book);
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while getting",
                    Message = ex.Message,
                });
            }
        }

        [HttpGet("category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBooksByCategory([FromRoute] int categoryId)
        {
            try
            {
                var books = _mapper.Map<List<BookDto>>(_bookRepository.GetBooksByCategory(categoryId));

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (books.Count == 0)
                {
                    return NotFound("Not Found Book");
                }

                return Ok(books);
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while getting",
                    Message = ex.Message,
                });
            }
        }

        [HttpPost("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateBook([FromBody] BookDto createBook,[FromRoute] int categoryId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_categoryRepository.CategoriesExists(categoryId))
                {
                    ModelState.AddModelError("", "Category is not exists");
                    return StatusCode(404, ModelState);
                }

                var isExists = _bookRepository
                                .GetBooks()
                                .Where(b => b.BookName.Trim().ToUpper() == createBook.BookName.Trim().ToUpper())
                                .FirstOrDefault();

                if (isExists != null)
                {
                    ModelState.AddModelError("", "Book already exists");
                    return StatusCode(409, ModelState);
                }

                var newBook = new Book()
                {
                    BookName = createBook.BookName,
                    InitQuantity = createBook.InitQuantity,
                    CurrentQuantity = createBook.CurrentQuantity,
                };

                _bookRepository.CreateBook(categoryId, newBook);

                return Ok("created successfully");
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while creating",
                    Message = ex.Message,
                });
            }
        }
    }
}
