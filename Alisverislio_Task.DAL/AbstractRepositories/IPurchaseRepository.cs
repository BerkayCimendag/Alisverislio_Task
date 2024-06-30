using Alisverislio_Task.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.AbstractRepositories
{
    public interface IPurchaseRepository : IRepository<Purchase>
    {
        Task<IEnumerable<Purchase>> GetPurchasesByUserIdAsync(int userId);
        Task<Purchase> GetPurchaseByUserAndBookIdAsync(int userId, int bookId);
    }
}
