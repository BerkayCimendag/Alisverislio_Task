using Alisverislio_Task.BLL.Dtos;
using Alisverislio_Task.DAL.Entities;
using Alisverislio_Task.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.AbstractServices
{
    public interface IUserService
    {
        Task<UserDto> Register(UserDto userDto);
        Task<UserDto> Login(LoginDto loginDto );
        Task<UserDto> GetProfileAsync(int userId);
        Task<UserDto> UpdateProfileAsync(int userId, UserDto userDto);
        Task<UserDto> UpdateUserRoleAsync(int adminUserId, int userId, UserRole newRole);
       Task<string> GenerateJwtToken(UserDto user);
    }
}
