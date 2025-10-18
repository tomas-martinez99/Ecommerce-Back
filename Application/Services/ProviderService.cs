using Application.CreateDtos;
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
    public class ProviderService : IProviderService
    {
        private readonly IProviderRepository _repo;
        private readonly IMapper _mapper;

        public ProviderService(IProviderRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProviderDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProviderDto>>(entities);
        }

        public async Task<ProviderDto> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null
                ? null
                : _mapper.Map<ProviderDto>(entity);
        }

        public async Task<ProviderDto> CreateAsync(CreateProviderDto dto)
        {
            var entity = _mapper.Map<Provider>(dto);
            await _repo.AddAsync(entity);
            return _mapper.Map<ProviderDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, CreateProviderDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            _mapper.Map(dto, entity!);
            await _repo.UpdateAsync(entity!);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null)
                return false;

            await _repo.DeleteAsync(entity);
            return true;
        }
    }
}
