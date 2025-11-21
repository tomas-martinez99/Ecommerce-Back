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
    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        private readonly EcommerceDbContext _DbContextProvider;
        public ProviderRepository(EcommerceDbContext context) : base(context)
        {
            _DbContextProvider = context;
        }
        public async Task<Provider> GetWithProductsAsync(int id)
        {
            return await _DbContextProvider.Set<Provider>()
                .Include(p => p.Products)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Provider>> GetAllWithProductsAsync()
        {
            return await _DbContextProvider.Set<Provider>()
                .Include(p => p.Products)
                .ToListAsync();
        }
    }
}
