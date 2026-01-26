using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Errors;
using FluentResults;
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

        public async Task<Result<IEnumerable<ProviderDto>>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();

            if (entities == null) return Result.Fail<IEnumerable<ProviderDto>>("An error ocurred when trying to get all provider");

            var response = _mapper.Map<IEnumerable<ProviderDto>>(entities);

            return Result.Ok(response);
        }

        public async Task<Result<ProviderDto>> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);

            if (entity == null) return Result.Fail<ProviderDto>(new NotFoundError($"Provider with id {id} not found"));

            var response = _mapper.Map<ProviderDto>(entity);

            return Result.Ok(response);
        }

        public async Task<Result<ProviderDto>> CreateAsync(CreateProviderDto dto)
        {
            var entity = _mapper.Map<Provider>(dto);

            await _repo.AddAsync(entity);

            await _repo.SaveChangesAsync();

            var response = _mapper.Map<ProviderDto>(entity);

            return Result.Ok(response);
        }

        public async Task<Result> UpdateAsync(int id, CreateProviderDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);

            if (entity == null) return Result.Fail(new NotFoundError($"Provider with id {id} not found"));

            _mapper.Map(dto, entity!);

            await _repo.UpdateAsync(entity!);

            await _repo.SaveChangesAsync();

            return Result.Ok();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);

            if (entity == null) return Result.Fail(new NotFoundError($"Provider with id {id} not found"));

            await _repo.DeleteAsync(entity);

            await _repo.SaveChangesAsync();

            return Result.Ok();
        }
        public async Task<Result<DetailProviderDto>> GetByIdWithProductsAsync(int id)
        {
            var entity = await _repo.GetWithProductsAsync(id);

            if (entity == null) return Result.Fail(new NotFoundError($"Provider with id {id} not found"));

            var response = _mapper.Map<DetailProviderDto>(entity);

            return Result.Ok(response);
        }

        public async Task<Result<IEnumerable<DetailProviderDto>>> GetAllWithProductsAsync()
        {
            var entities = await _repo.GetAllWithProductsAsync();

            if (entities == null) return Result.Fail<IEnumerable<DetailProviderDto>>("An error ocurred when trying to get all provider");

            var response = _mapper.Map<IEnumerable<DetailProviderDto>>(entities);

            return Result.Ok(response);
        }
    }
}
