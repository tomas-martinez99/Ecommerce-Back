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
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        private readonly EcommerceDbContext _DbContextProvider;
        public BrandRepository(EcommerceDbContext context) : base(context)
        {
            _DbContextProvider = context;
        }
    }
}
