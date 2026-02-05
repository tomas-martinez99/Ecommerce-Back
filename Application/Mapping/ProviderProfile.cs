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
    public class ProviderProfile : Profile
    {
        public ProviderProfile()
        {
            CreateMap<CreateProviderDto, Provider>();

            // Mapear Provider a ProviderDto básico
            CreateMap<Provider, ProviderDto>();

            // Mapear Provider a DetailProvider incluyendo la colección Products
            CreateMap<Provider, DetailProviderDto>()
                .ForMember(d => d.ProviderName, opt => opt.MapFrom(s => s.ProviderName))
                .ForMember(d => d.Products, opt => opt.MapFrom(s => s.Products));
        }
    }
}
