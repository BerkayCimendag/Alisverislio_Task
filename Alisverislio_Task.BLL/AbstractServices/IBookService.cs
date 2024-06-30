using Alisverislio_Task.BLL.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.AbstractServices
{
    public interface IBookService
    {
        Task<BookDto> AddBookAsync(BookDto bookDto, IFormFile imageFile);
        Task<BookDto> UpdateBookAsync(int id, BookDto bookDto);
        Task<bool> DeleteBookAsync(int id);
        Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm);
        Task<IEnumerable<BookDto>> GetBooksByLocationAsync(string shelf, string aisle, string floor, string section);
    }
}
