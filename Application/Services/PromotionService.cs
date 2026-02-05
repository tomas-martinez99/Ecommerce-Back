using Application.CreateDtos;
using Application.DetailDtos;
using Application.GetAllDtos;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IPromotionRepository _promotionRepo;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepo;

        public PromotionService(IProductRepository productRepo, IMapper mapper, IPromotionRepository promotionRepo)
        {
            _promotionRepo = promotionRepo;
            _mapper = mapper;
            _productRepo = productRepo;
        }

        public async Task<IEnumerable<PromotionDto>> GetActivePromotionsAsync(CancellationToken cancellationToken = default)
        {
            var promos = await _promotionRepo.GetActivePromotionsAsync(cancellationToken);
            return _mapper.Map<IEnumerable<PromotionDto>>(promos);
        }

        //Para logica interna
        public async Task<IEnumerable<Promotion>> GetActivePromotionsEntitiesAsync(CancellationToken cancellationToken = default)
        {
            return await _promotionRepo.GetActivePromotionsAsync(cancellationToken);
        }
        public async Task<IEnumerable<PromotionDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var promos = await _promotionRepo.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<PromotionDto>>(promos);
        }
        public async Task<DetailPromotionDto?> GetByIdAsync(int id)
        {
            var promo = await _promotionRepo.GetByIdAsync(id);
            return promo is null ? null : _mapper.Map<DetailPromotionDto>(promo);
        }

        public async Task<DetailPromotionDto> CreateAsync(CreatePromotionDto dto)
        {
            var entity = _mapper.Map<Promotion>(dto);

            // 🔹 Validar que los productos existen (opcional pero recomendable)
            var validProducts = await _productRepo.GetByIdsAsync(dto.ProductIds);
            entity.ProductPromotions = validProducts
                .Select(p => new ProductPromotion { ProductId = p.Id, Promotion = entity })
                .ToList();

            await _promotionRepo.AddAsync(entity);
            await _promotionRepo.SaveChangesAsync();

            return _mapper.Map<DetailPromotionDto>(entity);
        }

        public async Task<bool> UpdateAsync(int id, CreatePromotionDto dto)
        {
            var existing = await _promotionRepo.GetByIdAsync(id);
            if (existing == null) return false;

            // 🔹 Actualizar campos simples con AutoMapper
            _mapper.Map(dto, existing);

            // 🔹 Manejar relación muchos-a-muchos manualmente
            existing.ProductPromotions.Clear();
            foreach (var productId in dto.ProductIds.Distinct()) // evita duplicados
            {
                existing.ProductPromotions.Add(new ProductPromotion
                {
                    ProductId = productId,
                    PromotionId = existing.Id
                });
            }

            await _promotionRepo.UpdateAsync(existing);
            await _promotionRepo.SaveChangesAsync();
            return true;
        }



        public async Task<bool> DeleteAsync(int id)
        {
            var promo = await _promotionRepo.GetByIdAsync(id);
            if (promo == null) return false;

            await _promotionRepo.DeleteAsync(promo);
            await _promotionRepo.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SetEnabledAsync(int id, bool enabled)
        {
            var promo = await _promotionRepo.GetByIdAsync(id);
            if (promo == null) return false;

            promo.IsEnabled = enabled;
            await _promotionRepo.UpdateAsync(promo);
            await _promotionRepo.SaveChangesAsync();
            return true;
        }


    }
}