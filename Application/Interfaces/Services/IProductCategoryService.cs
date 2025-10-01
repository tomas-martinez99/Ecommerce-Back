using Application.CreateDtos;
using Application.DetailDto;
using Application.GetAllDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDto>> GetAllAsync();
        Task<DetailProductCategoryDto?> GetByIdsAsync(int productId, int categoryId);
        Task<IEnumerable<ProductCategoryDto>> GetByProductIdAsync(int productId);
        Task<IEnumerable<ProductCategoryDto>> GetByCategoryIdAsync(int categoryId);
        Task<ProductCategoryDto> CreateAsync(CreateProductCategoryDto dto);
        Task<bool> DeleteAsync(int productId, int categoryId);
    }
}
