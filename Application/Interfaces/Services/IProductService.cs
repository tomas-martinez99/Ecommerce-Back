using Application.Common;
using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDto>> GetAllAsync();
        Task<DetailProductDto> GetByIdAsync(int id);
        Task<DetailProductAdminDto> GetByIdAdminAsync(int id);
        Task<DetailProductDto> CreateAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(int id, CreateProductDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<ProductAdminDto>> GetAllFiltredAdminAsync(int? brandId, int? productGroupId, int? providerId);

        //Manejo de imagenes
        Task<ProductImageDto> AddImageAsync(int productId, IFormFile file, bool isMain = false);
        Task<bool> RemoveImageAsync(int productId, int imageId);
        Task<bool> SetMainImageAsync(int productId, int imageId);

        Task<PagedResult<ProductDto>> GetFilteredPagedAsync(int? brandId, int? productGroupId, int page, int pageSize);
    }
}
