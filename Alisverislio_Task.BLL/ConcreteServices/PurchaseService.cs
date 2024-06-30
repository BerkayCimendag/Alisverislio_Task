using Alisverislio_Task.BLL.AbstractServices;
using Alisverislio_Task.BLL.Dtos;
using Alisverislio_Task.DAL.AbstractRepositories;
using Alisverislio_Task.DAL.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.ConcreteServices
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PurchaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PurchaseDto> PurchaseBookAsync(int userId, int bookId)
        {
            var purchase = new Purchase
            {
                UserId = userId,
                BookId = bookId
            };

            await _unitOfWork.Purchases.AddAsync(purchase);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<PurchaseDto>(purchase);
        }
    }
}
