using Alisverislio_Task.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.AbstractRepositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<IEnumerable<Location>> SearchLocationsAsync(string shelf, string aisle, string floor, string section);
    }
}
