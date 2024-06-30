using Alisverislio_Task.BLL.Dtos;
using Alisverislio_Task.DAL.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alisverislio_Task.BLL.Maps
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<LoginDto, User>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            CreateMap<Note, NoteDto>().ReverseMap();
            CreateMap<Share, ShareDto>().ReverseMap();
            CreateMap<Purchase, PurchaseDto>().ReverseMap();
        }
    }
}
