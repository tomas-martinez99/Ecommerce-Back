using Application.CreateDtos;
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
            CreateMap<Provider, ProviderDto>();
        }
    }
}
