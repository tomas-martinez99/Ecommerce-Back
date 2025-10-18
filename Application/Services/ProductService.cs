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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        private readonly IProviderRepository _providerRepository;

        public ProductService(IProductRepository repo, IMapper mapper, IProviderRepository providerRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _providerRepository = providerRepository;
        }
        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(entities);
        }
        public async Task<IEnumerable<ProductDto>> GetAllProviderAsync()
        {
            var entities = await _repo.GetAllWithProviderAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(entities);
        }


        public async Task<DetailProductDto?> GetByIdAsync(int id)
        {
            var product = await _repo.GetByIdWithRelationsAsync(id) ?? await _repo.GetByIdAsync(id);
            return product is null ? null : _mapper.Map<DetailProductDto>(product);
        }

        public async Task<DetailProductDto> CreateAsync(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            entity.Provider = await _providerRepository.GetByIdAsync(dto.ProviderId);

            if (entity.Provider == null)
                throw new Exception($"No existe un proveedor con Id {dto.ProviderId}");
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return _mapper.Map<DetailProductDto>(entity);
        }

        public async Task<bool>UpdateAsync(int id, CreateProductDto dto)
        {
            var product = await _repo.GetByIdWithRelationsAsync(id) ?? await _repo.GetByIdAsync(id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            _mapper.Map(dto, product);           // copia valores del DTO sobre la entidad rastreada
            await _repo.SaveChangesAsync(); // persistir una sola vez
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null)
                return false;

            _repo.DeleteAsync(entity);
            await _repo.SaveChangesAsync();

            return true;
        }
    }
}
