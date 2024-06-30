using Alisverislio_Task.BLL.AbstractServices;
using Alisverislio_Task.BLL.Dtos;
using Alisverislio_Task.DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Alisverislio_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            var user = await _userService.Register(userDto);
            if (user == null)
                return BadRequest("User already exists");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userService.Login(loginDto);
            if (user == null)
                return Unauthorized("Invalid credentials");

            var token = await _userService.GenerateJwtToken(user);
            return Ok(new { token });
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _userService.GetProfileAsync(int.Parse(userId));
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(UserDto userDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var user = await _userService.UpdateProfileAsync(int.Parse(userId), userDto);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost("updateUserRole")]
        public async Task<IActionResult> UpdateUserRole(int userId, UserRole newRole)
        {
            var adminUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _userService.UpdateUserRoleAsync(int.Parse(adminUserId), userId, newRole);

            if (result == null)
            {
                return NotFound("User not found.");
            }

            return Ok(result);
        }
     
    }
}
