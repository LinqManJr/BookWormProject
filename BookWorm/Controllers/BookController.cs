using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookWorm.Core;
using BookWorm.Core.Context;
using BookWorm.Core.Models;
using BookWorm.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookWorm.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly UserManager<BookUser> _userManager;
        private readonly BookWormContext _context;

        public BookController(UserManager<BookUser> userManager,BookWormContext context) 
        {
            _userManager = userManager;
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> AddBookByUser([FromBody]ReadTableDto readTableDto)
        {
            var user = await _userManager.FindByIdAsync(readTableDto.UserId.ToString());
            var author = readTableDto.Book.Author;
            _context.Authors.Add(author);
            readTableDto.Book.Author = author;
            var readTable = new ReadTable
            {
                User = user,
                Book = readTableDto.Book,
                BookStatus = readTableDto.BookStatus,
                StartDate = readTableDto.StartDate,
                EndDate = readTableDto.EndDate,
                Rating = readTableDto.Rating,
                Resume = readTableDto.Resume
            };
            _context.Readings.Add(readTable);
            await _context.SaveChangesAsync();
            return Ok(readTable.Book);
            //check exceptions
            //TODO: add mapping from tdo to entity
        }
    }
}