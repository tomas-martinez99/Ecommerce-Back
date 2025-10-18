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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly EcommerceDbContext _DbContextProduct;
        public ProductRepository(EcommerceDbContext context) : base(context)
        {
            _DbContextProduct = context;
        }

        public async Task<IEnumerable<Product>> GetAllWithProviderAsync()
        {
            return await _DbContextProduct.Products
                .Include(p => p.Provider).ToListAsync();
        }
        public override async Task<Product> GetByIdAsync(int id)
        {
            return await _DbContextProduct.Products
                .Include(p => p.Provider)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product?> GetByIdWithRelationsAsync(int id)
        {
            return await _DbContextProduct.Products
                .Include(p => p.Provider)         // ejemplo de navegación
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
