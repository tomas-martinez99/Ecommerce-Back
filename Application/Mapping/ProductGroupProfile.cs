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
    public class ProductGroupProfile: Profile
    {
        public ProductGroupProfile() {
            CreateMap<CreateProductGroupDto, ProductGroup>();
            CreateMap<ProductGroup, ProductGroupDto>();
            CreateMap<ProductGroup, DetailProductGroupDto>();
        }
    }
}
