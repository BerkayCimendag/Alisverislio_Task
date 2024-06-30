using Alisverislio_Task.BLL.AbstractServices;
using Alisverislio_Task.BLL.Dtos;
using Alisverislio_Task.DAL.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Alisverislio_Task.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IConfiguration _configuration;

        public NoteController(INoteService noteService,IConfiguration configuration)
        {
            _noteService = noteService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> AddNoteAsync(NoteDto noteDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            noteDto.UserId = userId;
            var note = await _noteService.AddNoteAsync(noteDto);
            return Ok(note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNoteAsync(int id, NoteDto noteDto)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            noteDto.UserId = userId;
            var note = await _noteService.UpdateNoteAsync(id, noteDto);
            if (note == null)
                return NotFound();
            return Ok(note);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNoteAsync(int id)
        {
            var result = await _noteService.DeleteNoteAsync(id);
            if (!result)
                return NotFound();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteByIdAsync(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var note = await _noteService.GetNoteAsync(userId, id);
            if (note == null)
                return NotFound();
            return Ok(note);
        }

        [HttpGet("bybook/{bookId}")]
        public async Task<IActionResult> GetNotesByBookIdAsync(int bookId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var notes = await _noteService.GetNotesByBookIdAsync(userId, bookId);
            return Ok(notes);
        }

        [HttpGet("usernotes")]
        public async Task<IActionResult> GetUserNotesAsync()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var notes = await _noteService.GetUserNotesAsync(userId);
            return Ok(notes);
        }
    }
}
