using Alisverislio_Task.DAL.AbstractRepositories;
using Alisverislio_Task.DAL.Data;
using Alisverislio_Task.DAL.Entities;
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
    public class ShareRepository : Repository<Share>, IShareRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShareRepository(AppDbContext context,IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Share>> GetSharesByNoteIdAsync(int noteId)
        {
            var loggedInUserId = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value);

            return await _context.Shares
                .Where(s => s.NoteId == noteId && s.UserId== loggedInUserId ||s.Visibility == Enums.Visibility.Public)
                .ToListAsync();
        }

        public async Task<IEnumerable<Share>> GetSharesByUserIdAsync(int userId)
        {
            var loggedInUserId = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await _context.Shares
                .Where((s => s.UserId == userId && s.UserId == loggedInUserId ||s.Visibility==Enums.Visibility.Public))
                .ToListAsync();
        }
    }
}
