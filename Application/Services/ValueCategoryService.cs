using Application.CreateDtos;
using Application.DetailDtos;
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
    public class ValueCategoryService : IValueCategoryService
    {
        private readonly IValueCategoryRepository _repo;
        private readonly IMapper _mapper;
        
        public ValueCategoryService(IValueCategoryRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<List<ValueCategoryDto>> GetByCategoryIdAsync(int categoryId)
        {
            var values = await _repo.GetValuesByCategoryIdAsync(categoryId);
            return _mapper.Map<List<ValueCategoryDto>>(values);

        }
        public async Task<ValueCategoryDto> CreateAsync(CreateValueCategoryDto dto, int categoryId)
        {
            var entity = _mapper.Map<ValueCategory>(dto);
            entity.CategoryId = categoryId;

            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            return _mapper.Map<ValueCategoryDto>(entity);
        }

        public async Task<bool> DeleteAsync(int valueCategoryId)
        {
            var entity = await _repo.GetByIdAsync(valueCategoryId);
            if (entity is null) return false;

            await _repo.DeleteAsync(entity);
            await _repo.SaveChangesAsync();
            return true;
        }
    }
}
