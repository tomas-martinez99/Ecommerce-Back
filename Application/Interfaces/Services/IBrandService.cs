using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IBrandService
    {
        Task<List<BrandDto>> GetAllAsync();
        Task<DetailBrandDto?> GetByIdAsync(int id);
        Task<BrandDto> AddAsync(CreateBrandDto dto);
        Task UpdateAsync(CreateBrandDto dto, int id);
        Task DeleteAsync(int id);
    }
}
