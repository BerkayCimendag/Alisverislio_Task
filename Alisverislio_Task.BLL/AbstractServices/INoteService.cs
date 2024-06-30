using Alisverislio_Task.BLL.Dtos;
using Alisverislio_Task.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.AbstractServices
{
    public interface INoteService
    {
        Task<NoteDto> AddNoteAsync(NoteDto noteDto);
        Task<NoteDto> UpdateNoteAsync(int id, NoteDto noteDto);
        Task<bool> DeleteNoteAsync(int id);
        Task<NoteDto> GetNoteAsync(int userId, int noteId);
        Task<IEnumerable<NoteDto>> GetNotesByBookIdAsync(int userId, int bookId);
        Task<IEnumerable<NoteDto>> GetNotesByVisibilityAsync(int userId, Visibility visibility);
        Task<IEnumerable<NoteDto>> GetUserNotesAsync(int userId);
    }
}
