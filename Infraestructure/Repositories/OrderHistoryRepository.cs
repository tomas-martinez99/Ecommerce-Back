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
    public class OrderHistoryRepository : GenericRepository<OrderHistory> , IOrderHistoryRepository
    {
        private readonly EcommerceDbContext _context;

        public OrderHistoryRepository(EcommerceDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderHistory>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderHistory
                .Where(h => h.OrderId == orderId)
                .ToListAsync();
        }
    }
}
