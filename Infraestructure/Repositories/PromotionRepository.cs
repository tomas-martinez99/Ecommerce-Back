using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    using Application.Interfaces.Repositories;
    using Domain.Entities;
    using global::Infraestructure.Context;
    using Microsoft.EntityFrameworkCore;

    namespace Infraestructure.Repositories
    {
        public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
        {
            private readonly EcommerceDbContext _dbContext;

            public PromotionRepository(EcommerceDbContext dbContext) : base(dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<IEnumerable<Promotion>> GetActivePromotionsAsync(CancellationToken cancellationToken)
            {
                return await _dbContext.Promotions
                    .Where(p => p.IsEnabled)
                    .Include(p => p.ProductPromotions)
                        .ThenInclude(pp => pp.Product)
                        .ThenInclude(prod => prod.Brand)
                     .Include(p => p.ProductPromotions)
                        .ThenInclude(pp => pp.Product)
                        .ThenInclude(prod => prod.Images)
                    .Include(p => p.ProductPromotions)
                        .ThenInclude(pp => pp.Product)
                        .ThenInclude(prod => prod.Provider)
                    .Include(p => p.ProductPromotions)
                        .ThenInclude(pp => pp.Product)
                        .ThenInclude(prod => prod.ProductGroup)
                    .ToListAsync(cancellationToken);
            }

            public override async Task<Promotion?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
            {
                return await _dbContext.Promotions
                    .Include(p => p.ProductPromotions)
                        .ThenInclude(pp => pp.Product)
                        .ThenInclude(ppp => ppp.Brand)
                    .Include(p => p.ProductPromotions)
                        .ThenInclude(pp => pp.Product)
                        .ThenInclude(ppp => ppp.Provider)
                    .Include(p => p.ProductPromotions)
                        .ThenInclude(pp => pp.Product)
                        .ThenInclude(ppp => ppp.ProductGroup)
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            }

            public override async Task<IReadOnlyList<Promotion>> GetAllAsync(CancellationToken cancellationToken = default)
            {
                return await _dbContext.Promotions
                    .Include(p => p.ProductPromotions)
                        .ThenInclude(pp => pp.Product)
                            .ThenInclude(prod => prod.Brand)
                    .Include(p => p.ProductPromotions)
                        .ThenInclude(pp => pp.Product)
                            .ThenInclude(prod => prod.Images)
                    .Include(p => p.ProductPromotions)
                        .ThenInclude(pp => pp.Product)
                            .ThenInclude(prod => prod.ProductGroup)
                    .Include(p => p.ProductPromotions)
                        .ThenInclude(pp => pp.Product)
                            .ThenInclude(prod => prod.Provider)
                    .ToListAsync(cancellationToken);
            }


        }
    }

}