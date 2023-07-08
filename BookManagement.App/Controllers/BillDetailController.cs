using AutoMapper;
using BookManagement.App.Dto;
using BookManagement.App.Interfaces;
using BookManagement.App.Models;
using BookManagement.App.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.App.Controllers
{
    [ApiController]
    [Route("api/billDetail")]
    public class BillDetailController : Controller
    {
        private readonly IBillDetailRepository _billDetailRepository;
        private readonly IBillRepository _billRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BillDetailController(IBillDetailRepository billDetailRepository, IBillRepository billRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _billDetailRepository = billDetailRepository;
            _billRepository = billRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBillDetails()
        {
            try
            {
                var billDetails = _billDetailRepository
                                    .GetBillDetails()
                                    .Select(bill => new
                                    {
                                        Id = bill.Id,
                                        BookId = bill.BookId,
                                        BookName = bill.Book.BookName,
                                        Quantity = bill.Quantity,
                                    })
                                    .ToList();

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (billDetails.Count == 0)
                {
                    return NotFound("Not Found Bill");
                }

                return Ok(billDetails);
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while getting",
                    Message = ex.Message,
                });
            }
        }

        [HttpGet("{billDetailId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBillDetail([FromRoute] int billDetailId)
        {
            try
            {
                var bill = _billDetailRepository.GetBillDetail(billDetailId);

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (bill == null)
                {
                    return NotFound("Not Found Bill");
                }

                var response = new {
                    Id = bill.Id,
                    BookId = bill.BookId,
                    BookName = bill.Book.BookName,
                    Quantity = bill.Quantity
                };

                return Ok(response);
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
        public IActionResult GetBillDetailOfBill([FromRoute] int billId)
        {
            try
            {
                var bills = _billDetailRepository
                                .GetBillDetailOfBill(billId)
                                .Select(bill => new
                                {
                                    Id = bill.Id,
                                    BookId = bill.Book.Id,
                                    BookName = bill.Book.BookName,
                                    Quantity = bill.Quantity,
                                })
                                .ToList();

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (bills.Count == 0)
                {
                    return NotFound("Not Found Bill");
                }

                return Ok(bills);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateBillDetail([FromBody] BillDetailDto createBillDetail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!_billRepository.BillExists(createBillDetail.BillId))
                {
                    return NotFound("Not Found Bill");
                }

                if (!_bookRepository.BookExists(createBillDetail.BookId))
                {
                    return NotFound("Not Found Book");
                }

                if (createBillDetail.Quantity <= 0)
                {
                    return BadRequest("Quantity must be greater than zero");
                }

                var newBillDetail = new BillDetail()
                {
                    BillId = createBillDetail.BillId,
                    BookId = createBillDetail.BookId,
                    Quantity = createBillDetail.Quantity
                };

                _billDetailRepository.CreateBillDetail(newBillDetail);

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

        [HttpPut("{billDetailId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateBillDetail([FromRoute] int billDetailId, [FromBody] BillDetailDto updateBillDetail)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (billDetailId != updateBillDetail.Id)
                {
                    return BadRequest("Id is not match");
                }

                if (!_bookRepository.BookExists(updateBillDetail.BookId))
                {
                    return NotFound("Not Found Book");
                }

                if (!_billRepository.BillExists(updateBillDetail.BillId))
                {
                    return NotFound("Not Found Bill");
                }



                if (updateBillDetail.Quantity <= 0)
                {
                    return BadRequest("Quantity must be greater than zero");
                }

                var billDetailMap = _mapper.Map<BillDetail>(updateBillDetail);

                _billDetailRepository.UpdateBillDetail(billDetailMap);

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

        [HttpDelete("{billDetailId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteBillDetail([FromRoute] int billDetailId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var billToDelete = _billDetailRepository.GetBillDetail(billDetailId);

                if (billToDelete == null)
                {
                    return NotFound("Not Found Bill");
                }

                _billDetailRepository.DeleteBillDetail(billToDelete);

                return Ok("Deleted successfully");
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Title = "Something went wrong while updating",
                    Message = ex.Message,
                });
            }
        }
    }
}
