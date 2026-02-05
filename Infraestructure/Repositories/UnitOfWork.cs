using Application.Interfaces.Repositories;
using Domain.Entities;
using Infraestructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading;

namespace Infraestructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable, ITransactionalUnitOfWork
    {
        private readonly EcommerceDbContext _context;
        private IDbContextTransaction? _currentTransaction;
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
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Products = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            ProductImages = productImageRepository ?? throw new ArgumentNullException(nameof(productImageRepository));
            Providers = providerRepository ?? throw new ArgumentNullException(nameof(providerRepository));
            Brands = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            ProductGroups = productGroupRepository ?? throw new ArgumentNullException(nameof(productGroupRepository));
        }

        /// Guarda los cambios en la base de datos. Acepta CancellationToken.
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction != null)
                return _currentTransaction;
            _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            return _currentTransaction;
        }
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null) return;
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                await _currentTransaction.CommitAsync(cancellationToken);
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
        public async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_currentTransaction == null)
                return;
            try
            {
                await _currentTransaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }

        public void Dispose()
        {
            _currentTransaction?.Dispose();
            _context?.Dispose();
        }
    }
}
