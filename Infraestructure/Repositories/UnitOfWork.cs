using Application.Interfaces.Repositories;
using Domain.Entities;
using Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly EcommerceDbContext _context;

        public IProductRepository Products { get; }
        public IProductImageRepository ProductImages { get; }
        public IProviderRepository Providers { get; }
        public IBrandRepository Brands { get; }
        public IProductGroupRepository ProductGroups { get; }
        // 👈 Implementar

        public UnitOfWork(EcommerceDbContext context,
                          IProductRepository productRepository,
                          IProductImageRepository productImageRepository,
                          IProviderRepository providerRepository,
                          IProductGroupRepository productGroupRepository,
                          IBrandRepository brandRepository)
        {
            _context = context;
            Products = productRepository;
            ProductImages = productImageRepository;
            Providers = providerRepository;
            Brands = brandRepository;
            ProductGroups = productGroupRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
