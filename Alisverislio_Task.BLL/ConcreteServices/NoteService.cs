using Alisverislio_Task.BLL.AbstractServices;
using Alisverislio_Task.BLL.Dtos;
using Alisverislio_Task.DAL.AbstractRepositories;
using Alisverislio_Task.DAL.Entities;
using Alisverislio_Task.DAL.Enums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.ConcreteServices
{
    public class NoteService : INoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NoteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<NoteDto> AddNoteAsync(NoteDto noteDto)
        {
            var note = _mapper.Map<Note>(noteDto);
            await _unitOfWork.Notes.AddAsync(note);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<NoteDto>(note);
        }

        public async Task<NoteDto> UpdateNoteAsync(int id, NoteDto noteDto)
        {
            var note = await _unitOfWork.Notes.GetByIdAsync(id);
            if (note == null)
                return null;

            _mapper.Map(noteDto, note);
          await  _unitOfWork.Notes.UpdateAsync(note);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<NoteDto>(note);
        }

        public async Task<bool> DeleteNoteAsync(int id)
        {
            var note = await _unitOfWork.Notes.GetByIdAsync(id);
            if (note == null)
                return false;

          await  _unitOfWork.Notes.RemoveAsync(note);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<NoteDto> GetNoteAsync(int userId, int noteId)
        {
            var note = await _unitOfWork.Notes.GetByIdAsync(noteId);
            if (note == null || (note.UserId != userId && note.Visibility != Visibility.Public && !(await IsAdmin(userId))))
            {
                return null;
            }

            return _mapper.Map<NoteDto>(note);
        }

        public async Task<IEnumerable<NoteDto>> GetNotesByBookIdAsync(int userId, int bookId)
        {
            var notes = await _unitOfWork.Notes.GetNotesByBookIdAsync(bookId);
            if (!await IsAdmin(userId))
            {
                notes = notes.Where(n => n.UserId == userId || n.Visibility == Visibility.Public);
            }
            return _mapper.Map<IEnumerable<NoteDto>>(notes);
        }

        public async Task<IEnumerable<NoteDto>> GetNotesByVisibilityAsync(int userId, Visibility visibility)
        {
            var notes = await _unitOfWork.Notes.GetNotesByVisibilityAsync(visibility);
            if (!await IsAdmin(userId))
            {
                notes = notes.Where(n => n.UserId == userId || n.Visibility == Visibility.Public);
            }
            return _mapper.Map<IEnumerable<NoteDto>>(notes);
        }

        public async Task<IEnumerable<NoteDto>> GetUserNotesAsync(int userId)
        {
            var notes = await _unitOfWork.Notes.FindAsync(n => n.UserId == userId || n.Visibility == Visibility.Public);
            if (!await IsAdmin(userId))
            {
                notes = notes.Where(n => n.UserId == userId || n.Visibility == Visibility.Public);
            }
            return _mapper.Map<IEnumerable<NoteDto>>(notes);
        }

        private async Task<bool> IsAdmin(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            return user != null && user.Role == UserRole.Admin;
        }
    }
}
