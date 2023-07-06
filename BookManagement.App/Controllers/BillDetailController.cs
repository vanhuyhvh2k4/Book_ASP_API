﻿using AutoMapper;
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
        private readonly IMapper _mapper;

        public BillDetailController(IBillDetailRepository billDetailRepository, IMapper mapper)
        {
            _billDetailRepository = billDetailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBillDetails()
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
        }

        [HttpGet("{billDetailId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBillDetail([FromRoute] int billDetailId)
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
        }

        [HttpGet("bill/{billId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBillDetailOfBill([FromRoute] int billId)
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
        }
    }
}
