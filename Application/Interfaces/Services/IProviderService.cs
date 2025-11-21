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
    public interface IProviderService
    {
        Task<IEnumerable<ProviderDto>> GetAllAsync();
        Task<ProviderDto> GetByIdAsync(int id);
        Task<ProviderDto> CreateAsync(CreateProviderDto dto);
        Task<bool> UpdateAsync(int id, CreateProviderDto dto);
        Task<bool> DeleteAsync(int id);
        Task<DetailProviderDto> GetByIdWithProductsAsync(int id);
        Task<IEnumerable<DetailProviderDto>> GetAllWithProductsAsync();
    }
}
