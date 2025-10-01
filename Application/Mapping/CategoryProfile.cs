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
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, DetailValueWithCategorie>();
            CreateMap<CreateValueCategoryDto, ValueCategory>();
            CreateMap<ValueCategory, ValueCategoryDto>();
          
        }    
    }
}
