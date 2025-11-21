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
    public class BrandProfile : Profile
    {
        public BrandProfile() {
            CreateMap<CreateBrandDto, Brand>();
            CreateMap<Brand, BrandDto>();
            CreateMap<Brand, DetailBrandDto>();
        }
    }
}
