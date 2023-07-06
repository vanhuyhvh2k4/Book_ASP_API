using AutoMapper;
using BookManagement.App.Dto;
using BookManagement.App.Interfaces;
using BookManagement.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.App.Controllers
{
    [ApiController]
    [Route("api/reader")]
    public class ReaderController : Controller
    {
        private readonly IReaderRepository _readerRepository;
        private readonly IMapper _mapper;

        public ReaderController(IReaderRepository readerRepository, IMapper mapper)
        {
            _readerRepository = readerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetReaders()
        {
            var readers = _mapper.Map<List<ReaderDto>>(_readerRepository.GetReaders());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (readers == null)
            {
                return NotFound("Not Found Reader");
            }

            return Ok(readers);
        }

        [HttpGet("{readerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetReader([FromRoute] int readerId)
        {
            var reader = _mapper.Map<ReaderDto>(_readerRepository.GetReader(readerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (reader == null)
            {
                return NotFound("Not Found Reader");
            }

            return Ok(reader);
        }

        [HttpGet("bill/{billId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetReaderByBill([FromRoute] int billId)
        {
            var reader = _mapper.Map<ReaderDto>(_readerRepository.GetReaderByBill(billId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (reader == null)
            {
                return NotFound("Not Found Reader");
            }

            return Ok(reader);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateReader([FromBody] ReaderDto createReader)
        {
            if (createReader == null)
            {
                return BadRequest(ModelState);
            }

            var reader = new Reader()
            {
                FullName = createReader.FullName.Trim(),
                Email = createReader.Email.Trim(),
                Phone = createReader.Phone.Trim()
            };

            if(!_readerRepository.CreateReader(reader))
            {
                ModelState.AddModelError("", "Something went wrong while creating");
                return StatusCode(500, ModelState);
            }

            return Ok("created successfully");

        }
    }
}
