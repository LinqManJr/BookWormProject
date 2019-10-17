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
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> AddBookByUser(ReadTableDto readTableDto)
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
            return Ok();
            //check exceptions
            //check if book or author exist (out to service)
            //TODO: add mapping from tdo to entity
        }

        [HttpGet]
        public async Task<IEnumerable<object>> GetBooks(string userId)
        {            
            var result = _context.Readings.Where(x => x.User.Id == userId).Join(_context.Books, r => r.Book.Id, b => b.Id, (r, b) => new
            {
                BookId = b.Id,
                BookName = b.Name,
                BookAuthor = string.Join(b.Author.Name, b.Author.Surname)
            });
            return await Task.FromResult(result);            
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserBook(string userId, int bookId)
        {
            var readingsId = await _context.Readings.FirstOrDefaultAsync(x => x.User.Id == userId && x.Book.Id == bookId);
            if(readingsId != null)
            {
                _context.Readings.Remove(readingsId);
                await _context.SaveChangesAsync();
            }
            else
            {
                return BadRequest("Entity with this properties was not found");
            }
            return Ok($"Book {readingsId.Book.Name} was deleted");
        }
    }
}