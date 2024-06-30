using Alisverislio_Task.BLL.AbstractServices;
using Alisverislio_Task.BLL.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Alisverislio_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShareController : ControllerBase
    {
        private readonly IShareService _shareService;

        public ShareController(IShareService shareService)
        {
            _shareService = shareService;
        }

        [HttpPost("share")]
        public async Task<IActionResult> ShareNote([FromBody] ShareDto shareDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            shareDto.UserId = userId;
            var share = await _shareService.ShareNoteAsync(shareDto);
            if (share == null)
                return BadRequest("Note could not be shared.");

            return Ok(share);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetShareById(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var share = await _shareService.GetShareByIdAsync(userId, id);
            if (share == null)
                return NotFound();

            return Ok(share);
        }

        [HttpGet("note/{noteId}")]
        public async Task<IActionResult> GetSharesByNoteId(int noteId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var shares = await _shareService.GetSharesByNoteIdAsync(userId, noteId);
            return Ok(shares);
        }

        [HttpGet("user/{shareUserId}")]
        public async Task<IActionResult> GetSharesByUserId(int shareUserId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var shares = await _shareService.GetSharesByUserIdAsync(userId, shareUserId);
            return Ok(shares);
        }

        [HttpGet("usershares")]
        public async Task<IActionResult> GetUserSharesAsync()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var shares = await _shareService.GetUserSharesAsync(userId);
            return Ok(shares);
        }
    }
}
