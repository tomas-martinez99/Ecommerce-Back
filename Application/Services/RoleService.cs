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
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repo;
        private readonly IMapper _mapper;
        public RoleService(IRoleRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleDto>>(entities);
        }

        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null
                ? null
                : _mapper.Map<RoleDto>(entity);
        }

        // 3) Obtener un rol con su lista de usuarios
        public async Task<DetailUserWithRoleDto?> GetWithUsersByIdAsync(int id)
        {
            var entity = await _repo.GetByIdWithUsersAsync(id);                         
            return entity is null
                ? null
                : _mapper.Map<DetailUserWithRoleDto>(entity);
        }

        public async Task<RoleDto> CreateAsync(CreateRoleDto dto)
        {
            var entity = _mapper.Map<Role>(dto);
            await _repo.AddAsync(entity);
            await _repo.SaveChangesAsync();
            return _mapper.Map<RoleDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, CreateRoleDto dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return false;

            _mapper.Map(dto, entity);
            await _repo.UpdateAsync(entity);
            await _repo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return false;

            await _repo.DeleteAsync(entity);
            await _repo.SaveChangesAsync();
            return true;
        }
    }

}
