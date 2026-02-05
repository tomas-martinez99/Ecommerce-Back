using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
    public interface IPromotionService
    {
        //para logica interna
        Task<IEnumerable<Promotion>> GetActivePromotionsEntitiesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<PromotionDto>> GetActivePromotionsAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<PromotionDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<DetailPromotionDto?> GetByIdAsync(int id);
        Task<DetailPromotionDto> CreateAsync(CreatePromotionDto dto);
        Task<bool> UpdateAsync(int id, CreatePromotionDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> SetEnabledAsync(int id, bool enabled);
    }
}
