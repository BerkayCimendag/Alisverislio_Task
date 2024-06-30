using Alisverislio_Task.BLL.AbstractServices;
using Alisverislio_Task.BLL.Dtos;
using Alisverislio_Task.DAL.AbstractRepositories;
using Alisverislio_Task.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.ConcreteServices
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<BookDto> AddBookAsync(BookDto bookDto, IFormFile imageFile)
        {
            var book = _mapper.Map<Book>(bookDto);

            if (imageFile != null && imageFile.Length > 0)
            {
                var imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
                var imagePath = Path.Combine(imageDirectory, fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                book.ImageUrl = $"/images/{fileName}";
            }

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task<BookDto> UpdateBookAsync(int id, BookDto bookDto)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
                return null;

            book.Title = bookDto.Title;
            book.Author = bookDto.Author;
            book.Description = bookDto.Description;
            book.ISBN = bookDto.ISBN;
            book.LocationId = bookDto.LocationId;
            book.ImageUrl = bookDto.ImageUrl;

            var location = await _unitOfWork.Locations.GetByIdAsync(book.LocationId);
            if (location != null)
            {
                book.Location.Shelf = location.Shelf;
                book.Location.Aisle = location.Aisle;
                book.Location.Floor = location.Floor;
                book.Location.Section = location.Section;
            }

           await _unitOfWork.Books.UpdateAsync(book);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<BookDto>(book);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
                return false;

          await  _unitOfWork.Books.RemoveAsync(book);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm)
        {
            var books = await _unitOfWork.Books.SearchBooksAsync(searchTerm);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<IEnumerable<BookDto>> GetBooksByLocationAsync(string shelf, string aisle, string floor, string section)
        {
            var books = await _unitOfWork.Books.GetBooksByLocationAsync(shelf, aisle, floor, section);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }
    }
}
