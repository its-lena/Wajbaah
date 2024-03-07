using AutoMapper;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO;

namespace Wajbah_API
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<MenuItem, Menu_ItemDTO>().ReverseMap();
            CreateMap<MenuItem, Menu_ItemCreateDTO>().ReverseMap();
            CreateMap<MenuItem, Menu_ItemUpdateDTO>().ReverseMap();
            
            CreateMap<Chef, ChefDto>().ReverseMap();
            CreateMap<Chef, ChefCreateDto>().ReverseMap();
            CreateMap<Chef, ChefUpdateDto>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderCreateDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateDTO>().ReverseMap();
        }
    }
}
