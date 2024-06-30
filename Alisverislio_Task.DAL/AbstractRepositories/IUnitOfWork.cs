using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.DAL.AbstractRepositories
{
    public interface IUnitOfWork:IDisposable
    {
        IUserRepository Users { get; }
        IBookRepository Books { get; }
        INoteRepository Notes { get; }
        ILocationRepository Locations { get; }
        IShareRepository Shares { get; }
        IPurchaseRepository Purchases { get; }

        Task<int> CompleteAsync();
       
    }
}
