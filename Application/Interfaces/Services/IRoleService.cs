using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IRoleService
    { 
        Task<IEnumerable<RoleDto>> GetAllAsync();
        Task<RoleDto?> GetByIdAsync(int id);
        /// Devuelve los datos del rol, incluyendo la lista de usuarios asignados.
        Task<DetailUserWithRoleDto?> GetWithUsersByIdAsync(int id);
        Task<RoleDto> CreateAsync(CreateRoleDto dto);
        Task<bool> UpdateAsync(int id, CreateRoleDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
