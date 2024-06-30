using Alisverislio_Task.DAL.AbstractRepositories;
using Alisverislio_Task.DAL.Data;
using Alisverislio_Task.DAL.Entities;
using Alisverislio_Task.DAL.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.ConcreteRepositories
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NoteRepository(AppDbContext context,IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Note>> GetNotesByBookIdAsync(int bookId)
        {
            var userId =int.Parse( _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value);

            return await _context.Notes
                .Where(n => n.BookId == bookId &&
                            (n.Visibility != Visibility.Private || n.UserId == userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Note>> GetNotesByVisibilityAsync(Visibility visibility)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value);

            return await _context.Notes
                .Where(n => n.Visibility != Visibility.Private || n.UserId == userId)
                .ToListAsync();
        }
    }
}
