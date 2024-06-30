using Alisverislio_Task.BLL.AbstractServices;
using Alisverislio_Task.BLL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alisverislio_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("add")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddBook([FromForm] BookDto bookDto, IFormFile imageFile)
        {
            var book = await _bookService.AddBookAsync(bookDto, imageFile);
            if (book == null)
                return BadRequest("Book could not be added.");

            return Ok(book);
        }

        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDto bookDto)
        {
            var book = await _bookService.UpdateBookAsync(id, bookDto);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
                return NotFound();

            return Ok();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string searchTerm)
        {
            var books = await _bookService.SearchBooksAsync(searchTerm);
            return Ok(books);
        }

        [HttpGet("filterBooksByLocation")]
        public async Task<IActionResult> FilterBooksByLocation([FromQuery] string shelf, [FromQuery] string aisle, [FromQuery] string floor, [FromQuery] string section)
        {
            var books = await _bookService.GetBooksByLocationAsync(shelf, aisle, floor, section);
            return Ok(books);
        }
    }
}
