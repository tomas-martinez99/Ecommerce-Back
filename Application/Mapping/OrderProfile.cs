using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<CreateOrderDto, Order>()
            .ForMember(dest => dest.Products,
                       opt => opt.MapFrom(src => src.Products))
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.History, opt => opt.Ignore());

            CreateMap<Order, OrderDto>();

            CreateMap<Order, DetailOrderDto>();

            CreateMap<CreateOrderProductDto, OrderProduct>();

            CreateMap<OrderProduct, OrderProductDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));

            CreateMap<OrderHistory, OrderHistoryDto>();
        }
    }
}
