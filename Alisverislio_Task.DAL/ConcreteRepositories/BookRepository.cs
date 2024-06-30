using Alisverislio_Task.DAL.AbstractRepositories;
using Alisverislio_Task.DAL.Data;
using Alisverislio_Task.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.ConcreteRepositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BookRepository(AppDbContext context,IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
        {
            return await _context.Books
                .Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByLocationAsync(string shelf, string aisle, string floor, string section)
        {
            var query = _context.Books.Include(b => b.Location).AsQueryable();

            if (!string.IsNullOrEmpty(shelf))
                query = query.Where(b => b.Location.Shelf.Contains(shelf));
            if (!string.IsNullOrEmpty(aisle))
                query = query.Where(b => b.Location.Aisle.Contains(aisle));
            if (!string.IsNullOrEmpty(floor))
                query = query.Where(b => b.Location.Floor.Contains(floor));
            if (!string.IsNullOrEmpty(section))
                query = query.Where(b => b.Location.Section.Contains(section));

            return await query.ToListAsync();
        }
    }
}
