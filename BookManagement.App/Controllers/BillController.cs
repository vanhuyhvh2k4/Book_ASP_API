using AutoMapper;
using BookManagement.App.Data;
using BookManagement.App.Dto;
using BookManagement.App.Interfaces;
using BookManagement.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.App.Controllers
{
    [ApiController]
    [Route("api/bill")]
    public class BillController : Controller
    {
        private readonly IBillRepository _billRepository;
        private readonly IReaderRepository _readerRepository;
        private readonly IMapper _mapper;

        public BillController(IBillRepository billRepository, IReaderRepository readerRepository, IMapper mapper)
        {
            _billRepository = billRepository;
            _readerRepository = readerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBills()
        {
            var bills = _mapper.Map<List<BillDto>>(_billRepository.GetBills());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (bills.Count == 0)
            {
                return NotFound("Not Found Bill");
            }

            return Ok(bills);
        }

        [HttpGet("{billId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBill([FromRoute] int billId)
        {
            var bill = _mapper.Map<BillDto>(_billRepository.GetBill(billId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (bill == null)
            {
                return NotFound("Not Found Bill");
            }

            return Ok(bill);
        }

        [HttpGet("reader/{readerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBillsOfReader([FromRoute] int readerId)
        {
            var bills = _mapper.Map<List<BillDto>>(_billRepository.GetBillsOfReader(readerId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (bills.Count == 0)
            {
                return NotFound("Not Found Bill");
            }

            return Ok(bills);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateBill([FromBody] BillDto createBill)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_readerRepository.ReaderExists(createBill.ReaderId))
            {
                return NotFound("Not Found Reader");
            }

            var newBill = new Bill()
            {
                ReaderId = createBill.ReaderId,
                BorrowDate = createBill.BorrowDate,
            };

            if (!_billRepository.CreateBill(newBill))
            {
                ModelState.AddModelError("", "Something went wrong while creating");
                return StatusCode(500, ModelState);
            }

            return Ok("Created successfully");
        }
    }
}
