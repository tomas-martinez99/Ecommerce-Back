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
    public class ProductGroupService : IProductGroupService
    {
        private readonly IProductGroupRepository _repo;
        private readonly IMapper _mapper;

        public ProductGroupService(IProductGroupRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ProductGroupDto>> GetAllAsync() =>
            _mapper.Map<List<ProductGroupDto>>(await _repo.GetAllAsync());

        public async Task<DetailProductGroupDto?> GetByIdAsync(int id)
        {
            var productGroup = await _repo.GetByIdAsync(id);
            if (productGroup == null) return null;

            return _mapper.Map<DetailProductGroupDto>(productGroup);
        }
           
        public async Task<ProductGroupDto> AddAsync(CreateProductGroupDto dto)
        {
            var entity = _mapper.Map<ProductGroup>(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return _mapper.Map<ProductGroupDto>(entity);
        }

        public async Task UpdateAsync(CreateProductGroupDto dto, int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("Marca no encontrada");

            // 2. Mapear los cambios sobre la entidad existente
            _mapper.Map(dto, entity);

            // 3. Guardar cambios
            await _repo.UpdateAsync(entity);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return;
            await _repo.DeleteAsync(entity);
            await _repo.SaveChangesAsync();
        }
    }
}
