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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly EcommerceDbContext _DbContextProduct;
        public ProductRepository(EcommerceDbContext context) : base(context)
        {
            _DbContextProduct = context;
        }
    }
}
