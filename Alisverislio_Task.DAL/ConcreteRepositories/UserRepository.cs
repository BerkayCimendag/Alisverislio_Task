using Alisverislio_Task.DAL.AbstractRepositories;
using Alisverislio_Task.DAL.Data;
using Alisverislio_Task.DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.ConcreteRepositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(AppDbContext context,IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<User> SingleOrDefaultAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Set<User>().SingleOrDefaultAsync(predicate);
        }

        public async Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(predicate);
        }
    }
}
