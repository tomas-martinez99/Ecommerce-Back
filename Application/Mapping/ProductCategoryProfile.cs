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
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile(){
        CreateMap<CreateProductCategoryDto, ProductCategory>();
        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategory, DetailProductCategoryDto>()
            .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        }
    }
}
