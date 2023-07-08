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
            try
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
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while getting",
                    Message = ex.Message,
                });
            }
        }

        [HttpGet("{readerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetReader([FromRoute] int readerId)
        {
            try
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
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while getting",
                    Message = ex.Message,
                });
            }
        }

        [HttpGet("bill/{billId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetReaderByBill([FromRoute] int billId)
        {
            try
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
        public IActionResult CreateReader([FromBody] ReaderDto createReader)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var reader = new Reader()
                {
                    FullName = createReader.FullName.Trim(),
                    Email = createReader.Email.Trim(),
                    Phone = createReader.Phone.Trim()
                };

                _readerRepository.CreateReader(reader);

                return Ok("created successfully");
            } catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while creating",
                    Message = ex.Message,
                });
            }
        }

        [HttpPut("{readerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateReader([FromRoute] int readerId, [FromBody] ReaderDto updateReader)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_readerRepository.ReaderExists(readerId))
                {
                    return NotFound("Not Found Reader");
                }

                if (readerId != updateReader.Id)
                {
                    return BadRequest("Id is not match");
                }

                var readerMap = _mapper.Map<Reader>(updateReader);

                _readerRepository.UpdateReader(readerMap);

                return Ok("Updated successfully");

            } catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while updating",
                    Message = ex.Message,
                });
            }

        }

        [HttpDelete("{readerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteReader([FromRoute] int readerId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var readerToDelete = _readerRepository.GetReader(readerId);

                if (readerToDelete == null)
                {
                    return NotFound("Not Found Reader");
                }

                _readerRepository.DeleteReader(readerToDelete);

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
