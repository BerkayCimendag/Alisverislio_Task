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
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LocationRepository(AppDbContext context,IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Location>> SearchLocationsAsync(string shelf, string aisle, string floor, string section)
        {
            var query = _context.Locations.AsQueryable();

            if (!string.IsNullOrEmpty(shelf))
                query = query.Where(l => l.Shelf.Contains(shelf));
            if (!string.IsNullOrEmpty(aisle))
                query = query.Where(l => l.Aisle.Contains(aisle));
            if (!string.IsNullOrEmpty(floor))
                query = query.Where(l => l.Floor.Contains(floor));
            if (!string.IsNullOrEmpty(section))
                query = query.Where(l => l.Section.Contains(section));

            return await query.ToListAsync();
        }
    }
}
