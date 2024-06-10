using AutoMapper;
using Wajbah_API.Models;
using WajbahAdmin.Models.Dto;

namespace WajbahAdmin
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            

			CreateMap<Chef, ChefAdminDto>().ReverseMap();
       
            
            CreateMap<Customer, CustomerDto>().ReverseMap();
         

            CreateMap<Order, OrderDto>().ReverseMap();
           

            
            //CreateMap<ItemRateRecord, ItemRateRecordDto>().ReverseMap();
        }
    }
}
