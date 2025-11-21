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
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.ProductGroup, opt => opt.MapFrom(src => src.ProductGroup));

            CreateMap<Product, ProductAdminDto>()
              .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
               .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider))
               .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
               .ForMember(dest => dest.ProductGroup, opt => opt.MapFrom(src => src.ProductGroup));

            CreateMap<CreateProductDto, Product>()
            .ForMember(dest => dest.Provider, opt => opt.Ignore())
            .ForMember(dest => dest.Brand, opt => opt.Ignore())
            .ForMember(dest => dest.ProductGroup, opt => opt.Ignore());

            CreateMap<ProductImage, ProductImageDto>();

            CreateMap<Product, DetailProductDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.ProductGroup, opt => opt.MapFrom(src => src.ProductGroup));


            CreateMap<Product, DetailProductAdminDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(dest => dest.Provider, opt => opt.MapFrom(src => src.Provider))
            .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
            .ForMember(dest => dest.ProductGroup, opt => opt.MapFrom(src => src.ProductGroup));
        }
    }
}
