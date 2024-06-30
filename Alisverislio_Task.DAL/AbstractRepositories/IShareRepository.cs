using Alisverislio_Task.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.AbstractRepositories
{
    public interface IShareRepository : IRepository<Share>
    {
        Task<IEnumerable<Share>> GetSharesByNoteIdAsync(int noteId);
        Task<IEnumerable<Share>> GetSharesByUserIdAsync(int userId);
    }
}
