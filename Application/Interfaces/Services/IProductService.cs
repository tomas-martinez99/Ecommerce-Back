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
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<DetailProductDto> GetByIdAsync(int id);
        Task<DetailProductDto> CreateAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(int id, CreateProductDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<ProductDto>> GetAllProviderAsync();
    }
}
