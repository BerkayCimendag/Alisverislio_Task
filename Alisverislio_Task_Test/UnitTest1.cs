using Alisverislio_Task.BLL.AbstractServices;
using Alisverislio_Task.BLL.Dtos;
using Alisverislio_Task.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Security.Claims;

namespace Alisverislio_Task_Test
{
    public class NoteControllerTests
    {
        private readonly Mock<INoteService> _noteServiceMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly NoteController _controller;

        public NoteControllerTests()
        {
            _noteServiceMock = new Mock<INoteService>();
            _configurationMock = new Mock<IConfiguration>();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Role, "User")
            }, "mock"));

            _controller = new NoteController(_noteServiceMock.Object, _configurationMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = user }
                }
            };
        }

        [Fact]
        public async Task GetNoteByIdAsync_NoteExists_ReturnsOkResult()
        {
         
            var noteId = 1;
            var userId = 1;
            var note = new NoteDto { Id = noteId, Content = "Test Content", UserId = userId };

            _noteServiceMock.Setup(service => service.GetNoteAsync(userId, noteId))
                            .ReturnsAsync(note);

            
            var result = await _controller.GetNoteByIdAsync(noteId);

           
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedNote = Assert.IsType<NoteDto>(okResult.Value);
            Assert.Equal(noteId, returnedNote.Id);
        }

        [Fact]
        public async Task GetNoteByIdAsync_NoteDoesNotExist_ReturnsNotFoundResult()
        {
           
            var noteId = 1;
            var userId = 1;

            _noteServiceMock.Setup(service => service.GetNoteAsync(userId, noteId))
                            .ReturnsAsync((NoteDto)null);

          
            var result = await _controller.GetNoteByIdAsync(noteId);

           
            Assert.IsType<NotFoundResult>(result);
        }
    }
}