using Alisverislio_Task.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.AbstractServices
{
    public interface IShareService
    {
        Task<ShareDto> ShareNoteAsync(ShareDto shareDto);
        Task<ShareDto> GetShareByIdAsync(int userId, int id);
        Task<IEnumerable<ShareDto>> GetSharesByNoteIdAsync(int userId, int noteId);
        Task<IEnumerable<ShareDto>> GetSharesByUserIdAsync(int userId, int shareUserId);
        Task<IEnumerable<ShareDto>> GetUserSharesAsync(int userId);
    }
}
