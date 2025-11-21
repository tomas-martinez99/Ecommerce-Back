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
    public class BrandService: IBrandService
    {
        private readonly IBrandRepository _repo;
        private readonly IMapper _mapper;

        public BrandService(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<BrandDto>> GetAllAsync() =>
            _mapper.Map<List<BrandDto>>(await _repo.GetAllAsync());

        public async Task<DetailBrandDto> GetByIdAsync(int id)
        {
            var brand = await _repo.GetByIdAsync(id);
            if (brand == null) return null;

            return _mapper.Map<DetailBrandDto>(brand); // ✅ convierte Brand → BrandDto
        }

        public async Task<BrandDto> AddAsync(CreateBrandDto dto)
        {
            var entity = _mapper.Map<Brand>(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();

            // Mapear la entidad persistida a BrandDto
            return _mapper.Map<BrandDto>(entity);
        }

        public async Task UpdateAsync( CreateBrandDto dto, int id)
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
