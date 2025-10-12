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
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.Provider, opt => opt.Ignore());


            CreateMap<Product, ProductDto>();
               

            CreateMap<Provider, ProviderDto>();
        }
    }
}
