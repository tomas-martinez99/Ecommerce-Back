using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProductGroupService
    {
        Task<List<ProductGroupDto>> GetAllAsync();
        Task<DetailProductGroupDto?> GetByIdAsync(int id);
        Task<ProductGroupDto> AddAsync(CreateProductGroupDto dto);
        Task UpdateAsync(CreateProductGroupDto dto, int id);
        Task DeleteAsync(int id);
    }
}
