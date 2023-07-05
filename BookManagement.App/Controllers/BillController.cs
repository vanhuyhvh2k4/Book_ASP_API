using AutoMapper;
using BookManagement.App.Data;
using BookManagement.App.Dto;
using BookManagement.App.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.App.Controllers
{
    [ApiController]
    [Route("api/bill")]
    public class BillController : Controller
    {
        private readonly IBillRepository _billRepository;
        private readonly IMapper _mapper;

        public BillController(IBillRepository billRepository, IMapper mapper)
        {
            _billRepository = billRepository;
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
    }
}
