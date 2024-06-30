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
    public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PurchaseRepository(AppDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByUserIdAsync(int userId)
        {
            var loggedInUser = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (loggedInUser == userId)
            {

                return await _context.Purchases
                    .Where(p => p.UserId == userId)
                    .AsNoTracking()
                    .ToListAsync();
            }
            return null;
        }

        public async Task<Purchase> GetPurchaseByUserAndBookIdAsync(int userId, int bookId)
        {

            var loggedInUser = int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)!.Value!);
            if (loggedInUser == userId)
            {

                return await _context.Purchases
                .SingleOrDefaultAsync(p => p.UserId == userId && p.BookId == bookId);
            }
            return null ;
        }
    }
}
