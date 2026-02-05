using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Application.UpdateDto;
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
            CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Employed, opt => opt.MapFrom(src =>
        src.Employed != null
            ? src.Employed
            : new User { Id = 0, UserName = "Sin asignar" }))
            .ForMember(dest => dest.AppliedPromotions, opt => opt.MapFrom(src => src.AppliedPromotions))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<Order, ChangeStatusOrderDto>();


            CreateMap<Order, DetailOrderDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Employed, opt => opt.MapFrom(src =>
                    src.Employed != null
                ? src.Employed
                : new User { Id = 0, UserName = "Sin asignar" }))
                .ForMember(dest => dest.AppliedPromotions, opt => opt.MapFrom(src => src.AppliedPromotions))
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

            CreateMap<CreateOrderProductDto, OrderProduct>();

            CreateMap<OrderProduct, OrderProductDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));
            CreateMap<OrderHistory, OrderHistoryDto>();
            // Mapear UpdateOrderDto -> Order
            CreateMap<UpdateOrderDto, Order>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
                .ForMember(dest => dest.EmployedId, opt => opt.MapFrom(src => src.EmployedId));
            // Mapear UpdateOrderProductDto -> OrderProduct
            CreateMap<UpdateOrderProductDto, OrderProduct>()
                .ForMember(dest => dest.Product, opt => opt.Ignore());
            // 👆 ignoramos la navegación Product, porque solo mandás ProductId

            CreateMap<OrderAppliedPromotion, OrderAppliedPromotionDto>();
        }
    }
}
