using AutoMapper;
using Project.Common.Dto;
using Project.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class MapProFile: Profile
    {
        public MapProFile()
        {
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Request, RequestDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Task<Item>, Task<ItemDto>>().ReverseMap();
            CreateMap<Task<Request>, Task<RequestDto>>().ReverseMap();
            CreateMap<Task<User>, Task<UserDto>>().ReverseMap();
            CreateMap<Task<List<Item>>, Task<List<ItemDto>>>().ReverseMap();
            CreateMap<Task<List<Request>>, Task<List<RequestDto>>>().ReverseMap();
            CreateMap<Task<List<User>>, Task<List<UserDto>>>().ReverseMap();
        }
    }
}
