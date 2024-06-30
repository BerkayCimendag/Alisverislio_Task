using Alisverislio_Task.BLL.AbstractServices;
using Alisverislio_Task.BLL.Dtos;
using Alisverislio_Task.DAL.AbstractRepositories;
using Alisverislio_Task.DAL.Entities;
using Alisverislio_Task.DAL.Enums;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.ConcreteServices
{
    public class ShareService : IShareService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ShareService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ShareDto> ShareNoteAsync(ShareDto shareDto)
        {
            var purchase = await _unitOfWork.Purchases.GetPurchaseByUserAndBookIdAsync(shareDto.UserId, shareDto.BookId);
            if (purchase == null)
                return null; // Kitap satın almayan kullancı paylaşım yapamaz

            var share = _mapper.Map<Share>(shareDto);
            await _unitOfWork.Shares.AddAsync(share);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<ShareDto>(share);
        }

        public async Task<ShareDto> GetShareByIdAsync(int userId, int id)
        {
            var share = await _unitOfWork.Shares.GetByIdAsync(id);
            if (share == null || (share.UserId != userId && share.Visibility != Visibility.Public && !(await IsAdmin(userId))))
            {
                return null;
            }

            return _mapper.Map<ShareDto>(share);
        }

        public async Task<IEnumerable<ShareDto>> GetSharesByNoteIdAsync(int userId, int noteId)
        {
            var shares = await _unitOfWork.Shares.GetSharesByNoteIdAsync(noteId);
            if (!await IsAdmin(userId))
            {
                shares = shares.Where(s => s.UserId == userId || s.Visibility == Visibility.Public);
            }
            return _mapper.Map<IEnumerable<ShareDto>>(shares);
        }

        public async Task<IEnumerable<ShareDto>> GetSharesByUserIdAsync(int userId, int shareUserId)
        {
            var shares = await _unitOfWork.Shares.GetSharesByUserIdAsync(shareUserId);
            if (!await IsAdmin(userId))
            {
                shares = shares.Where(s => s.UserId == shareUserId || s.Visibility == Visibility.Public);
            }
            return _mapper.Map<IEnumerable<ShareDto>>(shares);
        }

        public async Task<IEnumerable<ShareDto>> GetUserSharesAsync(int userId)
        {
            var shares = await _unitOfWork.Shares.FindAsync(s => s.UserId == userId || s.Visibility == Visibility.Public);
            if (!await IsAdmin(userId))
            {
                shares = shares.Where(s => s.UserId == userId || s.Visibility == Visibility.Public);
            }
            return _mapper.Map<IEnumerable<ShareDto>>(shares);
        }

        private async Task<bool> IsAdmin(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            return user != null && user.Role == UserRole.Admin;
        }
    }
}
