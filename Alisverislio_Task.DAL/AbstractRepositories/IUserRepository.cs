using Alisverislio_Task.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.AbstractRepositories
{
    public interface IUserRepository : IRepository<User>
    {
      
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> SingleOrDefaultAsync(Expression<Func<User, bool>> predicate);
        Task<User> FirstOrDefaultAsync(Expression<Func<User, bool>> predicate);
    }
}
