using Alisverislio_Task.BLL.AbstractServices;
using Alisverislio_Task.BLL.Dtos;
using Alisverislio_Task.BLL.StaticMethods;
using Alisverislio_Task.DAL.AbstractRepositories;
using Alisverislio_Task.DAL.Data;
using Alisverislio_Task.DAL.Entities;
using Alisverislio_Task.DAL.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.ConcreteServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserDto> Register(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.Password = PasswordHelper.HashPassword(userDto.Password);
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> Login(LoginDto loginDto)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(u => u.Username == loginDto.Username);
            if (user == null)
            {
                return null;
            }

            if (user.Password == PasswordHelper.HashPassword(loginDto.Password))
            {

                var userDto = _mapper.Map<UserDto>(user);

                return userDto;
            }

            return null;
        }

        public async Task<UserDto> GetProfileAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateProfileAsync(int userId, UserDto userDto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
                return null;

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                userDto.Password = PasswordHelper.HashPassword(userDto.Password);
            }
            if (userDto.Role==UserRole.Admin)
            {
                userDto.Role = UserRole.RegularUser;
            }
            _mapper.Map(userDto, user);
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UserDto>(user);
        }


        public async Task<UserDto> UpdateUserRoleAsync(int adminUserId, int userId, UserRole newRole)
        {
            var adminUser = await _unitOfWork.Users.GetByIdAsync(adminUserId);
            if (adminUser == null || adminUser.Role != UserRole.Admin)
            {
                throw new UnauthorizedAccessException("Only admins can change user roles.");
            }

            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user == null)
            {
                return null;
            }

            user.Role = newRole;
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<UserDto>(user);
        }


        public async Task<string> GenerateJwtToken(UserDto user)
        {
            var findUser = await _unitOfWork.Users.FirstOrDefaultAsync(x => x.Name == user.Name);


            var claims = new[]
            {
                     new Claim(ClaimTypes.Name, findUser.Name),
                    new Claim(ClaimTypes.Role, findUser.Role.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, findUser.Id.ToString())
                };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
