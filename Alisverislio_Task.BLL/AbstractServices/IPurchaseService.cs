using Alisverislio_Task.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.AbstractServices
{
    public interface IPurchaseService
    {
        Task<PurchaseDto> PurchaseBookAsync(int userId, int bookId);
    }
}
