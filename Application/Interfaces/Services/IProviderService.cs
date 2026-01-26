using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IProviderService
    {
        Task<Result<IEnumerable<ProviderDto>>> GetAllAsync();
        Task<Result<ProviderDto>> GetByIdAsync(int id);
        Task<Result<ProviderDto>> CreateAsync(CreateProviderDto dto);
        Task<Result> UpdateAsync(int id, CreateProviderDto dto);
        Task<Result> DeleteAsync(int id);
        Task<Result<DetailProviderDto>> GetByIdWithProductsAsync(int id);
        Task<Result<IEnumerable<DetailProviderDto>>> GetAllWithProductsAsync();
    }
}
