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
    public class LocationService:ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<LocationDto> AddLocationAsync(LocationDto locationDto)
        {
            var location = _mapper.Map<Location>(locationDto);
            await _unitOfWork.Locations.AddAsync(location);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<LocationDto>(location);
        }
    }
}
