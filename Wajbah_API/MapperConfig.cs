﻿using AutoMapper;
using Wajbah_API.Models;
using Wajbah_API.Models.DTO;
using Wajbah_API.Models.DTO.Chats;
using Wajbah_API.Models.DTO.PromoCode;

namespace Wajbah_API
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<MenuItem, Menu_ItemDTO>().ReverseMap();
            CreateMap<MenuItem, Menu_ItemCreateDTO>().ReverseMap();
            CreateMap<MenuItem, Menu_ItemUpdateDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, OrderCreateDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateDTO>().ReverseMap();

            CreateMap<PromoCode, PromoCodeDTO>().ReverseMap();
            CreateMap<PromoCode, PromoCodeCreateDTO>().ReverseMap();
            CreateMap<PromoCode, PromoCodeUpdateDTO>().ReverseMap();

            CreateMap<Message, MessageDTO>().ReverseMap();
            CreateMap<Message, MessageCreateDTO>().ReverseMap();

            CreateMap<Conversation, ConversationDTO>().ReverseMap();
            CreateMap<Conversation, ConversationRequest>().ReverseMap();
        }
    }
}
