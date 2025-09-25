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
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateValueCategoryDto, ValueCategory>();
            CreateMap<ValueCategory, ValueCategoryDto>();
        }    
    }
}
