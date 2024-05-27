using AutoMapper;
using Wajbah_API.Models;
using WajbahAdmin.Models.Dto;

namespace WajbahAdmin
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            

			CreateMap<Chef, ChefDto>().ReverseMap();
       
            
            CreateMap<Customer, CustomerDto>().ReverseMap();
         

            CreateMap<Order, OrderDTO>().ReverseMap();
           

            
            //CreateMap<ItemRateRecord, ItemRateRecordDto>().ReverseMap();
        }
    }
}
