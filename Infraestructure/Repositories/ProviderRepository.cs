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
    public class ProviderRepository : GenericRepository<Provider>, IProviderRepository
    {
        private readonly EcommerceDbContext _DbContextProvider;
        public ProviderRepository(EcommerceDbContext context) : base(context)
        {
            _DbContextProvider = context;
        }
    }
}
