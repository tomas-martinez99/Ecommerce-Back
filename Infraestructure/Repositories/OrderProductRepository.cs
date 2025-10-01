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
    public class OrderProductRepository : GenericRepository<OrderProduct> , IOrderProductRepository
    {
        private readonly EcommerceDbContext _context;

        public OrderProductRepository(EcommerceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderProduct>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderProducts
                .Where(p => p.OrderId == orderId)
                .Include(p => p.Product)
                .ToListAsync();
        }
    }
}
