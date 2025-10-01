using Application.Interfaces.Repositories;
using Domain.Entities;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class ProductCategoryRepository : GenericRepository<ProductCategory>, IProductCategoryRepository
    {
        private readonly EcommerceDbContext _context;

        public ProductCategoryRepository(EcommerceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ProductCategory?> GetByIdsAsync(int productId, int categoryId)
        {
            return await _context.ProductCategories
                .Include(pc => pc.Product)
                .Include(pc => pc.Category)
                .FirstOrDefaultAsync(pc =>
                    pc.ProductId == productId &&
                    pc.CategoryId == categoryId);
        }

        public async Task<IEnumerable<ProductCategory>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductCategories
                .Where(pc => pc.ProductId == productId)
                .Include(pc => pc.Category)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductCategory>> GetByCategoryIdAsync(int categoryId)
        {
            return await _context.ProductCategories
                .Where(pc => pc.CategoryId == categoryId)
                .Include(pc => pc.Product)
                .ToListAsync();
        }
    }
}
