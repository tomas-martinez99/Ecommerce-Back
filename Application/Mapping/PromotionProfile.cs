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
    public class PromotionProfile : Profile
    {
        public PromotionProfile()
        {
            // 🔹 De CreatePromotionDto → Promotion
            CreateMap<CreatePromotionDto, Promotion>()
    .ForMember(dest => dest.ProductPromotions, opt => opt.Ignore());

            CreateMap<Promotion, PromotionDto>()
                     .ForMember(dest => dest.Products, opt => opt
                                 .MapFrom(src => src.ProductPromotions.Select(pp => pp.Product)))
                     .ForMember(dest => dest.IsEnabled, opt => opt
                                 .MapFrom(src => src.IsActive()));

            // 🔹 De Promotion → DetailPromotionDto (detalle completo)
            CreateMap<Promotion, DetailPromotionDto>()
                .ForMember(dest => dest.Products,
                           opt => opt.MapFrom(src => src.ProductPromotions.Select(pp => pp.Product)))
                .ForMember(dest => dest.IsEnabled,
                           opt => opt.MapFrom(src => src.IsActive()));
        }
    }
}