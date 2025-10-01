using Application.CreateDtos;
using Application.DetailDto;
using Application.GetAllDtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _repo;
        private readonly IMapper _mapper;

        public ProductCategoryService(IProductCategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductCategoryDto>>(list);
        }

        public async Task<DetailProductCategoryDto?> GetByIdsAsync(int productId, int categoryId)
        {
            var entity = await _repo.GetByIdsAsync(productId, categoryId);
            return entity is null
                ? null
                : _mapper.Map<DetailProductCategoryDto>(entity);
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetByProductIdAsync(int productId)
        {
            var list = await _repo.GetByProductIdAsync(productId);
            return _mapper.Map<IEnumerable<ProductCategoryDto>>(list);
        }

        public async Task<IEnumerable<ProductCategoryDto>> GetByCategoryIdAsync(int categoryId)
        {
            var list = await _repo.GetByCategoryIdAsync(categoryId);
            return _mapper.Map<IEnumerable<ProductCategoryDto>>(list);
        }

        public async Task<ProductCategoryDto> CreateAsync(CreateProductCategoryDto dto)
        {
            var entity = _mapper.Map<ProductCategory>(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return _mapper.Map<ProductCategoryDto>(entity);
        }

        public async Task<bool> DeleteAsync(int productId, int categoryId)
        {
            var entity = await _repo.GetByIdsAsync(productId, categoryId);
            if (entity is null) return false;
            await _repo.DeleteAsync(entity);
            await _repo.SaveChangesAsync();
            return true;
        }

    }
}