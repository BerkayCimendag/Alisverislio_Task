using Alisverislio_Task.DAL.Entities;
using Alisverislio_Task.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.AbstractRepositories
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<IEnumerable<Note>> GetNotesByBookIdAsync(int bookId);
        Task<IEnumerable<Note>> GetNotesByVisibilityAsync(Visibility visibility);
    }
}
