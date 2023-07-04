﻿using AutoMapper;
using BookManagement.App.Dto;
using BookManagement.App.Interfaces;
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
    }
}
