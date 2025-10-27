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
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
               .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider));

            CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.Provider, opt => opt.Ignore());
           
            CreateMap<ProductImage, ProductImageDto>();

            CreateMap<Product, DetailProductDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
                .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider));
        }
    }
}
