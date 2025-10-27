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
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {

        private readonly EcommerceDbContext _context;
        public ProductImageRepository(EcommerceDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<ProductImage>> GetByProductIdAsync(int productId)
        {
            return await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();
        }

        public async Task<ProductImage?> GetMainImageAsync(int productId)
        {
            return await _context.ProductImages
                .FirstOrDefaultAsync(pi => pi.ProductId == productId && pi.IsMain);
        }

        public async Task SetMainImageAsync(int productId, int imageId)
        {
            var images = await _context.ProductImages
                .Where(pi => pi.ProductId == productId)
                .ToListAsync();

            foreach (var img in images)
                img.IsMain = img.Id == imageId;

            _context.ProductImages.UpdateRange(images);
        }
    }
}
